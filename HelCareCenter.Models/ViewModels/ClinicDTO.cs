using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelCareCenter.Models.ViewModels
{
    public class ClinicDTO
    {
        public string Name { get; set; } = string.Empty;

       
        public int HelpCareCenterID { get; set; }
    }
}
