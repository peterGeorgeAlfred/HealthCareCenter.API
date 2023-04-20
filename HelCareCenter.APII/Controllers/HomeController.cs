using HelCareCenter.Context;
using HelCareCenter.Models.Entity;
using HelCareCenter.Models.ViewModels;
using HelCareCenter.Repositery.IManger;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HelCareCenter.APII.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly DBContext context;
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IPatient patientDB; 
        private IConfiguration configuration;

        public HomeController(UserManager<IdentityUser> _userManager, SignInManager<IdentityUser> _signInManager, RoleManager<IdentityRole> _roleManager, IConfiguration _configuration, IPatient _patentDB)
        {
            userManager = _userManager;
            signInManager = _signInManager;
            roleManager = _roleManager;           
            configuration = _configuration;
            patientDB = _patentDB;
        }


        ///  
        /// 1- recive full Object userName , Passworde ,confirmedPass,  ObjDonor , RoleName
        /// 2- find role by  RoleName To Get RoleID 
        /// 3- add user at table Users  with Role ID and send newUserID
        /// 4- check  if user Created Get UserID and add in Donor Table with UserID
        /// 5- check if user added in table Donor Then assign Role To User 
        ///

        #region Register
        [HttpPost("Register")] //api/Home/Regestir

        public async Task<ActionResult> RegestireShop(RegestireShopModel model)
        {


            try
            {
                UserMangerResponse userMangerResponse = new UserMangerResponse();
                if (!ModelState.IsValid)
                {
                    userMangerResponse.Message = "Model is Not Valid";
                    userMangerResponse.Errors = ModelState.Values.SelectMany(er => er.Errors).Select(ms => ms.ErrorMessage);
                    return BadRequest(userMangerResponse);
                }

                var role = await roleManager.FindByNameAsync("Patient");
                if (role == null)
                {
                    userMangerResponse.Message = "Cannot find Patient Role";
                    return NotFound(userMangerResponse);
                }


                var applicationUSer = new IdentityUser()
                {

                    Email = model.Email,
                    //UserName = $"{model.Patient_FirstName}{model.Patient_LastName}" ,
                    UserName = model.Email,
                };

                var result = await userManager.CreateAsync(applicationUSer, model.Password);
                if (result.Succeeded)
                {
                    var user = await userManager.FindByEmailAsync(model.Email);
                    if (user == null)
                    {
                        userMangerResponse.Message = $"Cannot Found patient By Email {user}";
                        return NotFound(userMangerResponse);
                    }
                    var patient = new Patient()
                    {
                        FirstName = model.Patient_FirstName,
                        User_Identity = user.Id,
                        LastName = model.Patient_LastName,
                        Gender = model.Patient_Gender,
                        MaritalStatus = model.Patient_MaritalStatus,
                        DOB = model.Patient_DOB,
                    };
                    var addedPatient = await patientDB.ADD(patient);

                    if (addedPatient == null)
                    {
                        await userManager.DeleteAsync(user);
                        userMangerResponse.Message = "Cannot Create Empty  Patient !!!";
                        userMangerResponse.Errors = result.Errors.Select(e => e.Description);
                        return BadRequest(userMangerResponse);
                    }

                } // if result Success    to  Create User 
                else
                {
                    userMangerResponse.Message = "Cannot Create Patient With Email And Password";
                    userMangerResponse.Errors = result.Errors.Select(e => e.Description);
                    return BadRequest(userMangerResponse);

                }


                var resultRole = await userManager.AddToRoleAsync(applicationUSer, "Patient");

                if (!resultRole.Succeeded)
                {
                    userMangerResponse.Message = $"Cannot Add PatientRole to Patient {resultRole}";
                    return BadRequest();
                }

                if (result.Succeeded)
                {
                    userMangerResponse.IsSuccess = result.Succeeded;
                    userMangerResponse.Message = "User Created Successfuly";
                    return Ok(userMangerResponse);

                }
                userMangerResponse.Message = "Cannot Create User";

                return BadRequest(userMangerResponse);




            }
            catch (Exception ex)
            {


                return BadRequest(new UserMangerResponse
                {
                    Message = $"Catch handling :: Cannot Register User :: ErrorMsg : {ex.Message}"
                });

            }


        } // Regestire 

      
        #endregion

        #region LoginWithTooken
        [HttpPost("Login")]  //POST : /api/ApplicationUser/Login
        public async Task<IActionResult> LoginWithTooken(LoginModel model)
        {
            // GET user By Email and Check if Null
            var user = await userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {

                return BadRequest(new ApiResponse
                {
                    Message = "There is no user with that Email address",
                    IsSuccess = false,
                });
            }

            // Check Password , Message Here For Demo , But we don't inform user the 
            // problem from email or from password
            var result = await userManager.CheckPasswordAsync(user, model.Password);
            if (!result)
                return BadRequest(new ApiResponse
                {
                    Message = "Invalid password",
                    IsSuccess = false,
                });




            //GET All Role For This User
            var userRole = await userManager.GetRolesAsync(user);

            // Create Claims To Aassign To Token will Created 
            var claims = new[]
            {

                new Claim("Email", model.Email),
                new Claim("ID", user.Id),
                new Claim(ClaimTypes.Role, userRole.First())

            };

            //Create KEY 
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["AuthSettings:Key"]));

            // Create Token OBJ
            var tokenOBj = new JwtSecurityToken
                (
                     claims: claims,
                     expires: DateTime.Now.AddDays(30),
                     //expires: DateTime.Now.AddSeconds(30),
                     signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)


                );

            // Create Tooken String
            string token = new JwtSecurityTokenHandler().WriteToken(tokenOBj);


            

            return Ok(new ApiResponse<AccessTokenResult>
            {
                Message = "Tooken Create Successfully",
                IsSuccess = true,
                Value = new()
                {
                    Token = token,
                    ExpiryDate = tokenOBj.ValidTo,


                }


            });


        }



        #endregion


        #region Logout
        [HttpGet("Logout")]
        public async Task Loguot()
        {
            await signInManager.SignOutAsync();
        }
        #endregion

      


    

      


       

       




    }//class
}
