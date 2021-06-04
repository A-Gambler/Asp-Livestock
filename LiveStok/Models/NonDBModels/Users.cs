using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LiveStok.Models.NonDBModels
{
    public class Users
    {
        public string Email { get; set; }
        public string Id { get; set; }
        [Display(Name="Email Confirmed")]
        public bool EmailConfirmed { get; set; }
        [Display(Name = "Is Locked")]
        public bool IsLocked { get; set; }
        public bool IsAdministrator { get; set; }
    }
}
