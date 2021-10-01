using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegistractionUser.Models
{
	public class ChangePassword
	{
		public string Id { get; set; }
		public string OldPassword { get; set; }
		public string NewPassword { get; set; }
	}
}
