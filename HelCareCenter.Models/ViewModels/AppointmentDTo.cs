using HelCareCenter.Models.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelCareCenter.Models.ViewModels
{
    public class AppointmentDTo
    {
       

        [ForeignKey("Doctor")]
        public int DoctorID { get; set; }      


        [ForeignKey("Patient")]
        public int PatientId { get; set; }
       

        public DateTime From { get; set; }
        public DateTime To { get; set; }

        public string Status { get; set; } = string.Empty;

        public bool IsAccepted { get; set; }

    }
}
