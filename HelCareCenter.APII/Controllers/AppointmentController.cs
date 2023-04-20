using HelCareCenter.Models.Entity;
using HelCareCenter.Models.ViewModels;
using HelCareCenter.Repositery.IManger;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using HelCareCenter.SendEmail;
using Microsoft.AspNetCore.Authorization;

namespace HelCareCenter.APII.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
   
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointment AppointmentDB;
        private readonly IPatient PatientDB;
        private readonly UserManager<IdentityUser> userManager;

        public AppointmentController(IAppointment _AppointmentDB, IPatient patientDB, UserManager<IdentityUser> userManager)
        {
            AppointmentDB = _AppointmentDB;
            PatientDB = patientDB;
            this.userManager = userManager;
        }
        [HttpGet]
        public virtual async Task<ActionResult<IEnumerable<Appointment>>> GetAll()
        {
            try
            {
                return await AppointmentDB.GetAll();

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost("BookAppointment")]
        public virtual async Task<ActionResult<Appointment>> Post(AppointmentDTo model)
        {
            try
            {

                Appointment appointment = new Appointment();
                appointment.DoctorID = model.DoctorID;
                appointment.PatientId = model.PatientId;
                appointment.From = model.From;
                appointment.To = model.To; 

                    

                var result = await AppointmentDB.ADD(appointment);
                return Created("Created Successfully", result);

            }
            catch
            {

                return Problem();

            }


        }

        [HttpPost("CheckAppontmentAvilability")]
        public virtual async Task<ActionResult> checkAvilability(AppointmentDTo model)
        {
            try
            {


                var result = await AppointmentDB.CheckAvilability(model.DoctorID , model.From , model.To);
                if (result) return Ok("Avilable ");

                return Ok("Not Avilable"); 

            }
            catch
            {

                return Problem();

            }


        }

        [HttpPatch("AcceptAppointment")]
        [Authorize(Roles ="admin")]
        public virtual async Task<ActionResult> AcceptAppointment(AppointmentDTo model)
        {
            try
            {

                Appointment appointment = new Appointment() ;

                appointment.DoctorID = model.DoctorID;
                appointment.PatientId = model.PatientId;
                appointment.Status = model.Status;
                appointment.isAccepted = model.IsAccepted;
                appointment.From = model.From;
                appointment.To = model.To; 



                var result = await AppointmentDB.Accept(appointment);
                if (result != null && result.ID != 0 && result.isAccepted == true)
                {
                    var pateint =  await  PatientDB.GetById(result.PatientId);
                    var user = await userManager.FindByIdAsync(pateint.User_Identity);
                    MailStore mailStore = new MailStore();
                    mailStore.Email = user.Email;
                    mailStore.Name = user.UserName;
                    mailStore.Message = "your appointment is Accepted succcessfully"; 
                    
                    await SendEmail.MailManger.sendEmailFn(mailStore);
                    return Ok("Accepted successfully ");

                }

                return Ok("Not Accepted");

            }
            catch
            {

                return Problem();

            }


        }

        [HttpPatch("RejectAppointment")]
        [Authorize("admin")]

        public virtual async Task<ActionResult> RejectAppointment(Appointment model)
        {
            try
            {


                var result = await AppointmentDB.Accept(model);
                if (model != null && result.isAccepted == false)
                {
                    var pateint = await PatientDB.GetById(model.PatientId);
                    var user = await userManager.FindByIdAsync(pateint.User_Identity);
                    MailStore mailStore = new MailStore();
                    mailStore.Email = user.Email;
                    mailStore.Name = user.UserName;
                    mailStore.Message = "Sorry, your appointment is rejected";

                    await SendEmail.MailManger.sendEmailFn(mailStore);
                    

                }

                return Ok("appointment is rejected");

            }
            catch
            {

                return Problem();

            }


        }

    }
}
