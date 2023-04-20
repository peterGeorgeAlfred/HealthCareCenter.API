using HelCareCenter.Context;
using HelCareCenter.Models.Entity;
using HelCareCenter.Repositery.IGeneral;
using HelCareCenter.Repositery.IManger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelCareCenter.Repositery.Manger
{
    public class HelpCareCenrterSQL : General<HelpCareCenter>, IHelpCenterCare
    {

        public HelpCareCenrterSQL(DBContext _db) : base(_db)
        {

        }

        


       
    }// Class

}
