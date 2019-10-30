﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MemberShipAPI.Models
{
    public class Login
    {
        [Required]
        [MaxLength(200)]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}