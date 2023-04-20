using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelCareCenter.Models.Entity
{
    public class Clinic
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        [ForeignKey("HelpCareCenter")]
        public int HelpCareCenterID { get; set; }
        public virtual HelpCareCenter HelpCareCenter { get; set; }

    }
}
