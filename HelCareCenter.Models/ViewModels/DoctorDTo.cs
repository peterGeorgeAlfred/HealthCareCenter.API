using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelCareCenter.Models.ViewModels
{
    public class DoctorDTo
    {
        public string Name { get; set; } = string.Empty;   
        public int ClinicId { get; set; }
    }
}
