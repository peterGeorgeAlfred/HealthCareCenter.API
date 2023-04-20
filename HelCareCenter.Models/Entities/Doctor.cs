﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelCareCenter.Models.Entity
{
    public class Doctor
    {
        [Key]
        public int ID { get; set; }
        public string  Name { get; set; }  = string.Empty;

        [ForeignKey("Clinic")]
        public int ClinicId { get; set; }
        public virtual Clinic Clinic { get; set; }

    }
}
