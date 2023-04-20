﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelCareCenter.Models.ViewModels
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Please Enter Your Valid Email")]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
