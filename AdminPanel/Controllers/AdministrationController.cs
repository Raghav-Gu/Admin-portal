using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RegistractionUser.Models;
using System;
using System.Threading.Tasks;

namespace RegistractionUser.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdministrationController : ControllerBase
    {
       
        private readonly RoleManager<IdentityRole> _roleManager;
        public AdministrationController(RoleManager<IdentityRole>roleManager)
        {
            
            _roleManager = roleManager;
           
        }

        [HttpPost("createrole")]
        public async Task<IActionResult> CreateRole(RoleManagment role)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    IdentityRole identityRole = new IdentityRole
                    {
                        Name = role.RoleName
                    };
                    var result = await _roleManager.CreateAsync(identityRole);
                    if (result.Succeeded)
                    {
                        return Ok(new Response { Message ="Role Created!"});
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

        [HttpGet("getRoleList")]
        public IActionResult GetRoleList()
        {
            try
            {
                var result = _roleManager.Roles;
                return Ok(result);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpGet("editrole/{id}")]
        public async Task<IActionResult> GetRoleById(string Id)
        {
            try
            {
                var result = await _roleManager.FindByIdAsync(Id);
                if(result == null)
                {
                    return BadRequest(new Response {Message = "failed!"});
                }
                else
                {
                    var update = new RoleManagment
                    {
                        RoleName = result.Name
                    };

                    return Ok(update);
                    
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpPost("editrole")]
        public async Task<IActionResult> EditRole(RoleManagment model)
        {
            try
            {
                var role = await _roleManager.FindByIdAsync(model.Id);
                if (role == null)
                {
                    return BadRequest(new Response { Message = "failed!" });
                }
                else
                {
                    role.Name = model.RoleName;
                    role.NormalizedName = model.RoleName;
                    var result = await _roleManager.UpdateAsync(role);
                    if (result.Succeeded)
                    {
                        return Ok(new Response { Message = " Role Update sucessfully! " });
                    }
                    else
                    {
                        return BadRequest(new Response { Message = "failed!" });
                    }
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpDelete("deleterole/{Id}")]
        public async Task<IActionResult> DeleteRole(string Id)
        {
            try
            {
                var role = await _roleManager.FindByIdAsync(Id);
                if (role == null)
                    return BadRequest(new Response { Message = "failed!" });
                var result = await _roleManager.DeleteAsync(role);
                if (result.Succeeded)
                    return Ok(new Response { Message = "Role deleted!" });
                return BadRequest(new Response { Message = "failed!" });
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
