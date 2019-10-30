using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MemberShipAPI.Models
{
    public class User
    {
        [Required]
        [MaxLength(200)]
        public string UserName { get; set; }

        [Required]       
        public string Password { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(256)]
        public string SecurityQuestion { get; set; }
        [Required]
        [MaxLength(128)]
        public string SecurityAnswer { get; set; }

        public bool IsApproved { get; set; }
    }
}