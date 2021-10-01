using Microsoft.AspNetCore.Authentication;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegistractionUser.Models
{
    public class LoginModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }

    }

    public class ActiveUser
    {
        public string Id { get; set; }
        public bool IsActive { get; set; }
    }
}
