using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelCareCenter.Models.Entity
{
    public class HelpCareCenter
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty; 
        public string  LogoPath { get; set; } = string.Empty;

        public string LongT { get; set; } = string.Empty;

        public string Latit { get; set; } = string.Empty;

        public ICollection<Clinic> Clinics { get; set; } = new HashSet<Clinic>(); 
    }
}
