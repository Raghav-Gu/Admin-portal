using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RegistractionUser.Models
{
	public class UserResetPassword
	{
		public string uid { get; set; }
		public string Password { get; set; }

		[Compare("Password")]
		public string ConfirmPassword { get; set; }
	}
}
