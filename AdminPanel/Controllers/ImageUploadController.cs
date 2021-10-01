using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RegistractionUser.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RegistractionUser.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageUploadController : ControllerBase
    {
        private static IWebHostEnvironment _environment;
       
        public ImageUploadController(IWebHostEnvironment environment)
        {
            _environment = environment;
          
        }

        [HttpPost("ImagePost")]
        public async Task<string> ImagePost([FromForm]ImageUpload objImage)
        {
            try
            {
                if (objImage.files.Length > 0)
                {
                    if (!Directory.Exists(_environment.WebRootPath + "\\Upload\\"))
                    {
                        Directory.CreateDirectory(_environment.WebRootPath + "\\Upload\\");
                    }
                    using (FileStream fileStream = System.IO.File.Create(_environment.WebRootPath + "\\Upload\\" + objImage.files.FileName))
                    {
                        objImage.files.CopyTo(fileStream);
                        fileStream.Flush();
                        return "\\Upload\\" + objImage.files.FileName;
                    }
          
             }

                else
                {
                    return "failed";
                }
            }
            catch (Exception ex)
            {

                return ex.Message.ToString();
            }
           
            
        }

    }
}
