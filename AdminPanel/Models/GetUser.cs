using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegistractionUser.Models
{
    public class GetUser
    {
        //public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobileNo { get;set; }
        public string Email { get; set; }
    }
}
