using AutoMapper;
using HelCareCenter.Models.Entity;
using HelCareCenter.Models.ViewModels;
using HelCareCenter.Repositery.IGeneral;
using HelCareCenter.Repositery.IManger;
using Microsoft.AspNetCore.Mvc;

namespace HelCareCenter.APII.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class HelpCareCenterController : ControllerBase
    {
        private readonly IHelpCenterCare GeneralDB;
        public HelpCareCenterController(IHelpCenterCare _GeneralDB)
        {
            GeneralDB = _GeneralDB;
          
        }
        [HttpGet]
        public virtual async Task<ActionResult<IEnumerable<HelpCareCenter>>> GetAll()
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
        public virtual async Task<ActionResult<HelpCareCenter>> Post(HelpCareCenterDTo model)
        {
            try
            {

                HelpCareCenter helpCareCenter = new HelpCareCenter();
                helpCareCenter.Name = model.Name; 
                helpCareCenter.LogoPath = model.LogoPath;
                helpCareCenter.Latit = model.Latit;
                helpCareCenter.LongT = model.LongT; 

                var result =  await GeneralDB.ADD(helpCareCenter); 
                return Created("Created Successfully", result);

            }
            catch
            {

                return Problem();

            }


        }



    }
}
