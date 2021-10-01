using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RegistractionUser.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace RegistractionUser.Interface
{
    public interface IAccountRepositry
    {
        Task<IdentityResult> CreateUserAsync(RegisterModel model);
        Task<SignInResult> LoginUser(LoginModel model);
        Task<IdentityResult> ConfirmEmail(string uid, string token);
        
        Task ForgotPasswordAsync(ForgotPassword model);
        Task<IdentityResult> UpdateUserAsync(RegisterModel model);

        Task<IdentityResult> ChangePasswordAsync(ChangePassword model);

        JwtToken GenerateJwt(LoginModel model);
 
       
    }
}
