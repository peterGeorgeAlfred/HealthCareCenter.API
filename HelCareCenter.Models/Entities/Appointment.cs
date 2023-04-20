using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelCareCenter.Models.Entity
{
    public class Appointment
    {
        [Key]
        public int ID { get; set; }

        [ForeignKey("Doctor")]
        public int DoctorID { get; set; }
        public virtual Doctor Doctor { get; set; }


        [ForeignKey("Patient")]
        public int PatientId { get; set; }
        public virtual Patient Patient { get; set; }

        public DateTime From { get; set; }
        public DateTime To { get; set; }

        public string Status { get; set; } = string.Empty;

        public bool isAccepted { get; set; }
    }
}
