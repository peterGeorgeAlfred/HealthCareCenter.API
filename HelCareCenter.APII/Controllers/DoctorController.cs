using HelCareCenter.Models.Entity;
using HelCareCenter.Models.ViewModels;
using HelCareCenter.Repositery.IManger;
using Microsoft.AspNetCore.Mvc;
using System.Numerics;

namespace HelCareCenter.APII.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class DoctorController : ControllerBase
    {
        private readonly IDoctor GeneralDB;
        public DoctorController(IDoctor _GeneralDB)
        {
            GeneralDB = _GeneralDB;

        }

        [HttpGet]
        public virtual async Task<ActionResult<IEnumerable<Doctor>>> GetAll()
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


        [HttpGet("DoctorsByClinicId{id}")]
        public virtual async Task<ActionResult<IEnumerable<Doctor>>> GetAllDoctorByClinicId(int id)
        {
            try
            {
                return await GeneralDB.GetAllDoctorByClinicID(id);

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }


        [HttpPost]
        public virtual async Task<ActionResult<Doctor>> Post(DoctorDTo model)
        {
            try
            {

                Doctor doctor = new Doctor();
                doctor.Name = model.Name;
                doctor.ClinicId = model.ClinicId; 
                var result = await GeneralDB.ADD(doctor);
                return Created("Created Successfully", result);

            }
            catch
            {

                return Problem();

            }


        }

    

    }
}
