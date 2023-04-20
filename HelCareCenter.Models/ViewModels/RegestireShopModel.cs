using HelCareCenter.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelCareCenter.Models.ViewModels
{
    public class RegestireShopModel
    {

        public string Patient_FirstName { get; set; } = string.Empty;

        public string Patient_LastName { get; set; } = string.Empty;

        public DateTime Patient_DOB { get; set; }

        public Gender Patient_Gender { get; set; }

        public MaritalStatus Patient_MaritalStatus { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; } = String.Empty;

        [MinLength(8)]
        [Required]
        public string Password { get; set; } = String.Empty;

        [Required]
        [Compare("Password")]
        public string ConfirmedPassword { get; set; } = String.Empty;


    }
}
