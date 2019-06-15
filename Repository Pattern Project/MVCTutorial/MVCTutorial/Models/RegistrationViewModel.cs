using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCTutorial.Models
{
    public class RegistrationViewModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string EmailId { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public Nullable<int> RoleId { get; set; }
    }
}