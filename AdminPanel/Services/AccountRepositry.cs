using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RegistractionUser.Interface;
using RegistractionUser.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace RegistractionUser.Services
{
    public class AccountRepositry : IAccountRepositry
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;
        public AccountRepositry(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
           IConfiguration configuration)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._configuration = configuration;
        }

        public async Task<IdentityResult> CreateUserAsync(RegisterModel model)
        {

            var user = new ApplicationUser()
            {
                UserName = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                MobileNo = model.MobileNo,
                Email = model.Email,
               
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {

                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                if (!string.IsNullOrEmpty(token))
                {
                    await SendEmailConformation(user, token);
                }

            }
            return result;
        }

        public async Task<Microsoft.AspNetCore.Identity.SignInResult> LoginUser(LoginModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
            return result;
        }

        private async Task SendEmailConformation(ApplicationUser user, string token)
        {
            string appdomain = _configuration.GetSection("Application:AppDomain").Value;
            string conformationlink = _configuration.GetSection("Application:EmailConformation").Value;
            UserEmailOptions options = new UserEmailOptions
            {
                ToEmails = new List<string> { user.Email },
                Placeholder = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("{{Link}}",string.Format(appdomain + conformationlink,user.Id,token))
                }
            };
            foreach (var item in options.Placeholder)
            {
                try
                {
                    var smsBody = $"hey {user.FirstName}, Thank you for register Admin-Portal.Please go to the register email and Confirm your Account!";
                    List<string> test = new List<string>();
                    int chunkSize = 160;
                    for (int i = 0; i < smsBody.Length; i = i + chunkSize)
                    {
                        if (smsBody.Length - i >= chunkSize)
                            test.Add(smsBody.Substring(i, chunkSize));
                        else
                            test.Add(smsBody.Substring(i, ((smsBody.Length - i))));
                    }
                    foreach (var Messagebody in test)
                    {
                        var SMSBody = Messagebody;
                        SendMessage(SMSBody, user.MobileNo);
                    }

                    SmtpClient smtp = new SmtpClient();
                    string subject = "Admin-Portal";
                    string body = $"<div class='item' style='border: 1px solid black;background: moccasin;'><div class='picture'><img src='~/Upload/etelligens1.jpg' alt='Etelligens' style='width: 130px;position:absolute;'><h1 style='text-align: center;font-size: 40px;'>Welcome!</h1><div class='items' style='text-align: center;color: gray;font-size: 16px; '><p>We're excited to have you get started. First, you need to confirm your account. Just press the button below.</p></div><div class='button' style='text-align: center;'><a href ='{item.Value}'><input class='btn btn-danger' type='button' value='Confirm Account' style='background-color:orange;width: 150px;height: 45px;font-size:17px;'></a><p style='font-size:16px; color: gray; margin-right:180px'>If you have any questions, just reply to this email—we're always happy to help out.</p> <p style ='color:gray;padding:10px;font-size:16px;margin-right:640px;'>Cheers,<br>Admin-Portal Team</p> <p style ='color:gray;margin-bottom:2px'> &copy; 2021 Admin - portal.com </p></div></div> ";
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.Credentials = new System.Net.NetworkCredential("Raghavg11296@gmail.com", "MummyPapa@123");
                    smtp.EnableSsl = true;
                    smtp.Timeout = 500000000;
                    MailMessage msg = new MailMessage();
                    msg.Subject = subject;
                    msg.IsBodyHtml = true;
                    msg.Body = body;

                    string toaddress = $"{user.Email}";
                    string[] strArray = new string[] { toaddress };
                    foreach (string str in strArray)
                    msg.To.Add(str);
                    string fromaddress = "raghavg11296@gmail.com";
                    msg.From = new MailAddress(fromaddress);
                    smtp.Send(msg);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

        }

        public void SendMessage(string body, string conact)
        {
            string accountSid = "AC97cda6d1ee5530894d0a63efd15cc3e6";
            string authToken = "51575e8e16988ea375a9d90633c926dd";
            try
            {
                if (!conact.Contains("+"))
                {
                    conact = "+91" + conact;
                }

                TwilioClient.Init(accountSid, authToken);
                var message = MessageResource.Create(
                    body: body,
                    from: new Twilio.Types.PhoneNumber("+13252389079"),
                    to: new Twilio.Types.PhoneNumber(conact)
                );

            }
            catch (Exception ex)
            {

                throw ex;

            }

        }

        public async Task<IdentityResult> ConfirmEmail(string uid, string token)
        {
            return await _userManager.ConfirmEmailAsync(await _userManager.FindByIdAsync(uid), token);

        }

        public async Task ForgotPasswordAsync(ForgotPassword model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                SendEmailForgotPassword(user);
            }

        }

		private void SendEmailForgotPassword(ApplicationUser user)
		{
			string appdomain = _configuration.GetSection("ApplicationData:AppDomain").Value;
			string conformationlink = _configuration.GetSection("ApplicationData:ForgotPasswordLink").Value;
			UserEmailOptions options = new UserEmailOptions
			{
				ToEmails = new List<string> { user.Email },
				Placeholder = new List<KeyValuePair<string, string>>()
				{
					new KeyValuePair<string, string>("{{Link}}",string.Format(appdomain + conformationlink,user.Id))
				}
			};

			foreach (var item in options.Placeholder)
			{
				SmtpClient smtp = new SmtpClient();
				string subject = "Admin-Portal";
				var body = $"<div class='item' style='border: 1px solid black;background: moccasin;'><div class='picture'><img src='~/Upload/etelligens1.jpg' alt='Etelligens' style='width: 130px;position:absolute;'><h1 style='text-align: center;font-size: 40px;'>Welcome!</h1><div class='items' style='text-align: center;color: gray;font-size: 16px; '><p>We're excited to have you get started. First, you need to confirm your account. Just press the button below.</p></div><div class='button' style='text-align: center;'><a href ='{item.Value}'><input class='btn btn-danger' type='button' value='Confirm Account' style='background-color:orange;width: 150px;height: 45px;font-size:17px;'></a><p style='font-size:16px; color: gray; margin-right:180px'>If you have any questions, just reply to this email—we're always happy to help out.</p> <p style ='color:gray;padding:10px;font-size:16px;margin-right:640px;'>Cheers,<br>Admin-Portal Team</p> <p style ='color:gray;margin-bottom:2px'> &copy; 2021 Admin - portal.com </p></div></div> "; ;
				smtp.Host = "smtp.gmail.com";
				smtp.Port = 587;
				smtp.Credentials = new System.Net.NetworkCredential("Raghavg11296@gmail.com", "MummyPapa@123");
				smtp.EnableSsl = true;
				smtp.Timeout = 500000000;
				MailMessage msg = new MailMessage();
				msg.Subject = subject;
				msg.IsBodyHtml = true;
				msg.Body = body;
				string toaddress = $"{user.Email}";
				string[] strArray = new string[] { toaddress };
				foreach (string str in strArray)
					msg.To.Add(str);
				string fromaddress = "raghavg11296@gmail.com";
				msg.From = new MailAddress(fromaddress);
				smtp.Send(msg);
			}
		}

		public async Task<IdentityResult> UpdateUserAsync(RegisterModel model)
        {
            var user = await _userManager.FindByIdAsync(model.Id);
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.MobileNo = model.MobileNo;

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                string body = $"<p>Your profile has been updated sucessfully!</p>";
                string subject = "Admin-Portal";
                SendEmail(body , subject,user);
            }
            return result;

        }
        public async Task<IdentityResult> ChangePasswordAsync(ChangePassword model)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(model.Id);
                var updatePassword = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                if (updatePassword.Succeeded)
                {
                    string body = $"<p>Your password has been changed sucessfully!</p>";
                    string subject = "Admin-Portal";
                    SendEmail(body, subject, user);
                }
                return updatePassword;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void SendEmail(string body,string subject,ApplicationUser user)
        {

            SmtpClient smtp = new SmtpClient();
            string Subject = subject;
            var Body =body;
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.Credentials = new System.Net.NetworkCredential("Raghavg11296@gmail.com", "MummyPapa@123");
            smtp.EnableSsl = true;
            smtp.Timeout = 500000000;
            MailMessage msg = new MailMessage();
            msg.Subject = subject;
            msg.IsBodyHtml = true;
            msg.Body = body;
            string toaddress = $"{user.Email}";
            string[] strArray = new string[] { toaddress };
            foreach (string str in strArray)
            msg.To.Add(str);
            string fromaddress = "raghavg11296@gmail.com";
            msg.From = new MailAddress(fromaddress);
            smtp.Send(msg);
        }

        public JwtToken GenerateJwt(LoginModel model)
        {
            var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, model.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            var token = new JwtSecurityToken
            (
                 issuer: _configuration["JWT:ValidIssuer"],
                 audience: _configuration["JWT:ValidAudience"],
                 expires: DateTime.Now.AddHours(3),
                 claims: authClaims,
                 signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            var jwttoken = new JwtToken
            {
                RefreshToken = new RefreshTokenGenerator().GenerateRefreshToken(32),
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo
            };

            return jwttoken;
        }

		
	}

}


