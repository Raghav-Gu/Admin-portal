using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RegistractionUser.Interface;
using RegistractionUser.Models;
using System;
using System.Linq;
using System.Threading.Tasks;


namespace RegistractionUser.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepositry _accountRepositry;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
       
        public AccountController(IConfiguration configuration, UserManager<ApplicationUser> userManager, IAccountRepositry accountRepositry)
        {
            _userManager = userManager;
            _accountRepositry = accountRepositry;
            _configuration = configuration;
           
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody]RegisterModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _accountRepositry.CreateUserAsync(model);
                    if (!result.Succeeded)
                    {
                        var modeldata = new Response
                        {
                            Message = "Not Register!"
                        };
                        return BadRequest(modeldata);
                    }
                    else
                    {
                        var modeldata = new Response
                        {
                            Message = "Registered Successfully!"
                        };
                        return Ok(modeldata);
                    }
                }
                return Ok();
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        [HttpPost]
        [Route("forgotpassword")]
        public async Task<IActionResult> ForgotPassword(ForgotPassword model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await _userManager.FindByEmailAsync(model.Email); //get the user by Email
                    if (user == null)
                    {
                        return BadRequest("Your email or username is incorrect");
                    }

                    var result = _accountRepositry.ForgotPasswordAsync(model);

                }
            }
            catch (Exception ex)
            {
                throw ex;

            }

            return NoContent();
        }

        [HttpGet("ForgotPasswordLink")]
        public async Task<IActionResult> ResetPassword(string uid)
        {
            if (uid == null)
            {
                return BadRequest("Not valid");
            }

            var user = await _userManager.FindByIdAsync(uid); //Get the user by userId
            if (user == null)
            {
                return BadRequest("Not valid");
            }

            return Ok("Valid");
        }

        [HttpPost("ForgotPasswordLink")]
        public async Task<IActionResult> ResetPassword(UserResetPassword model)
        {
            var user = await _userManager.FindByIdAsync(model.uid); //Get the user by UserId
            if (user != null)
            {
                try
                {
                    var code = await _userManager.GeneratePasswordResetTokenAsync(user); //Generate Reset Token of Reset Password
                    var result = await _userManager.ResetPasswordAsync(user, code, model.Password);
                    if (result.Succeeded)
                    {
                        return NoContent();
                    }
                    else
                    {
                        return BadRequest();
                    }

                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
            return BadRequest("Not valid");
        }

        //     [Route("Login")]
        //     [HttpPost]
        //     public async Task<IActionResult> Login(LoginModel model)
        //     {
        //try
        //{
        //var user = await _userManager.FindByEmailAsync();
        //             if (ModelState.IsValid)
        //             { 
        //                 var result = await _accountRepositry.LoginUser(model);
        //                 if (result.Succeeded)
        //                 {

        //                     return Ok(user);

        //                 }
        //                 else
        //                 {
        //                     var modeldata = new Response
        //                     {
        //                         Message = "Login failed! "
        //                     };
        //                     return BadRequest(modeldata);
        //                 }
        //             }
        //             return Ok(user);
        //         }
        //         catch(Exception ex)
        //{
        //             throw ex;
        //}

        //     }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody]LoginModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                user = await _userManager.FindByNameAsync(model.Email);
                if (user == null)
                {
                    return Unauthorized();
                }
            }

            if (!user.EmailConfirmed)
            {
                return Unauthorized();
            }

            var result = await _userManager.CheckPasswordAsync(user, model.Password);
            if (result == true)
            {
                return Ok(new
                {
                    token = _accountRepositry.GenerateJwt(model),
                    user.Id
                });
            }
            return Unauthorized();
        }

       
        [HttpGet("confirmEmail")]
        public async Task<IActionResult> emailverification(string uid, string token)
        {

            try
            {
                if (!string.IsNullOrEmpty(uid) && !string.IsNullOrEmpty(token))
                {
                    token = token.Replace(' ', '+');
                    var result = await _accountRepositry.ConfirmEmail(uid, token);
                    if (result.Succeeded)
                    {

                        return Ok(new Response { Message = "Email verification sucessfully!" });
                    }
                    else
                    {

                        return BadRequest(new Response { Message = "Email not verified!" });
                    }

                }
                return Ok();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword(ChangePassword model)
        {
			try
			{
				if (ModelState.IsValid)
				{
                    var result = await _accountRepositry.ChangePasswordAsync(model);
                    if (result.Succeeded)
                    {
                        return Ok(new Response { Message = "Password change sucessfully!" });
                    }
                    else
                    {
                        return BadRequest(new Response { Message = "Failed!" });
                    }
				}
                return Ok();
			}
			catch (Exception ex)
			{

				throw ex;
			}
			//var user = await _userManager.FindByIdAsync(model.Id);
			//if (user != null)
			//{
			//	try
			//	{
			//		var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
			//		if (result.Succeeded)
			//		{

			//			return Ok(new Response { Message = "Password update sucessfully!" });
			//		}
			//		else
			//		{

			//			return BadRequest(new Response { Message = "Password not updated!" });
			//		}

			//	}
			//	catch (Exception ex)
			//	{
			//		throw ex;
			//    }

		 //   }

			//	return Ok();
	    }

        [HttpGet("GetUpdateUser/{Id}")]
        public async Task<IActionResult> GetUpdateUser(string Id)
        {
            var user = await _userManager.FindByIdAsync(Id);
            if (user == null)
            {
                return BadRequest();
            }
            var result = new RegisterModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                MobileNo = user.MobileNo,
                Email = user.Email

            };
            return Ok(result);
        }
        [HttpPost("UpdateUser")]
        public async Task<IActionResult> UpdateUser(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _accountRepositry.UpdateUserAsync(model);
                    if (result.Succeeded)
                    {

                        return Ok(new Response { Message = "Updated sucessfully!" });
                    }
                    else
                    {
                        return BadRequest(new Response { Message = "User profile not Updated!" });
                    }
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
            return Ok();

        }

        [Route("google-login")]
        [HttpGet]
        public IActionResult GoogleLogin()
        {
            var properties = new AuthenticationProperties
            {
                RedirectUri = Url.Action("GoogleResponse")
            };
            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }

        [Route("google-response")]
        public async Task<IActionResult> GoogleResponse()
        {
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            var claims = result.Principal.Identities.FirstOrDefault()
                               .Claims.Select(claim => new
                               {
                                   claim.Issuer,
                                   claim.OriginalIssuer,
                                   claim.Type,
                                   claim.Value
                               });
            return Ok(claims);
        }
    }
}



