using HelCareCenter.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelCareCenter.Models.Entity
{
    public class Patient
    {
        [Key]
        public int ID { get; set; }
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public DateTime DOB { get; set; }

        public Gender Gender { get; set; }

        public MaritalStatus MaritalStatus { get; set; }


        public string User_Identity { get; set; } = string.Empty; 



    }
}
