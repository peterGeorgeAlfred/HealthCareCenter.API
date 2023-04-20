using HelCareCenter.Models.Entity;
using HelCareCenter.Models.ViewModels;
using HelCareCenter.Repositery.IManger;
using Microsoft.AspNetCore.Mvc;

namespace HelCareCenter.APII.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ClinicController : ControllerBase
    {
        private readonly IClinic GeneralDB;
        public ClinicController(IClinic _GeneralDB)
        {
            GeneralDB = _GeneralDB;

        }
        [HttpGet]
        public virtual async Task<ActionResult<IEnumerable<Clinic>>> GetAll()
        {
            try
            {
                return await GeneralDB.GetAll();

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public virtual async Task<ActionResult<Clinic>> Post(ClinicDTO model)
        {
            try
            {

                Clinic clinic = new();
                clinic.Name = model.Name;
                clinic.HelpCareCenterID = model.HelpCareCenterID; 

                var result = await GeneralDB.ADD(clinic);
                return Created("Created Successfully", result);

            }
            catch
            {

                return Problem();

            }


        }



    }
}
