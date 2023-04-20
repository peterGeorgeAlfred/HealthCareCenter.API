using HelCareCenter.Context;
using HelCareCenter.Models.Entity;
using HelCareCenter.Repositery.IGeneral;
using HelCareCenter.Repositery.IManger;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelCareCenter.Repositery.Manger
{
    public class ClinicSQL : General<Clinic>, IClinic
    {

        public ClinicSQL(DBContext _db) : base(_db)
        {

        }
        
    }



}
