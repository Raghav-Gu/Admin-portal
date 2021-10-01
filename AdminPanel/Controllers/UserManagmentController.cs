using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RegistractionUser.Models;
using System;
using System.Threading.Tasks;


namespace RegistractionUser.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserManagmentController : ControllerBase
    {
        
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;

        public UserManagmentController(IConfiguration configuration, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _configuration = configuration;
        }


        [HttpGet("GetUserList")]
        [Authorize]
        public IActionResult GetUserListAsync()
        {
            try
            {
                var user = _userManager.Users;
                if (user == null)
                {
                    return BadRequest();
                }
                else
                {
                    return Ok(user);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("EditUser/{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            try
            {

                var user = await _userManager.FindByIdAsync(id);
                if (user == null)
                {
                    return BadRequest();
                }
                else
                {
                    var result = new GetUser
                    {
                       
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        MobileNo = user.MobileNo,
                        Email = user.Email

                    };

                    return Ok(result);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
           
        }

        [HttpPost("EditUser")]
        public async Task<IActionResult> EditUser(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userManager.FindByIdAsync(model.Id);
                    user.FirstName = model.FirstName;
                    user.LastName = model.LastName;
                    user.MobileNo = model.MobileNo;
                    user.Email = model.Email;
                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return Ok("Update Sucessfully!");
                    }
                    else
                    {
                        return BadRequest("Not Updated!");
                    }

                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
            return Ok();
        }

        [HttpDelete("Deleteuser/{Id}")]
        public async Task<IActionResult> DeleteUser(string Id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(Id);
                if (user == null)
                {
                    return BadRequest();
                }
                else
                {
                    var result = await _userManager.DeleteAsync(user);
                    if (result.Succeeded)
                    {
                        return Ok(new Response { Message ="User delete Sucessfully!"});
                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return Ok();
        }

        [HttpPost("AddUser")]
        public async Task<IActionResult> AddUser(RegisterModel model)
         {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = new ApplicationUser
                    {
                        UserName = model.FirstName,  
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        MobileNo = model.MobileNo,
                        Email = model.Email
                        
                    };

                    var result = await _userManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        return Ok("User Add Sucessfully!");
                    }
                    else
                    {
                        return BadRequest();
                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return Ok();
        }

        [HttpPost("UserActive")]
        public async Task<IActionResult> UserActive(ActiveUser model)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(model.Id);
                if (user.IsActive == null || user.IsActive == false)
                {
                    user.IsActive = model.IsActive;
                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return Ok(new Response { Message = "User is activated!" });
                    }
                    else
                    {
                        return BadRequest(new Response { Message = "Failed!" });
                    }
                }
                else if (user.IsActive == true)
                {
                    user.IsActive = model.IsActive;
                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return Ok(new Response { Message = "User is deactivated!" });
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
        }


    }
}
