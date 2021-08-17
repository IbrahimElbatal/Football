using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace Football.Api.Helpers
{
    public class FileHelper
    {
        public  static async Task<string> SaveImageOrLogo(IHostEnvironment environment,IFormFile file, string prefix)
        {
            var directory = Path.Combine(environment.ContentRootPath,"wwwroot", "Images");
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            var fileName = prefix + "_" + Guid.NewGuid() + Path.GetExtension(file.FileName);
            var stream = new FileStream(Path.Combine(directory, fileName), FileMode.Create, FileAccess.ReadWrite);
            await file.CopyToAsync(stream);
            var logoUrl = "Images/" + fileName;
            return logoUrl;
        }
    }
}
