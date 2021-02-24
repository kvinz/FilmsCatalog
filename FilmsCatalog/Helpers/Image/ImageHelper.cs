using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FilmsCatalog.Helpers.Image
{
    public class ImageHelper
    {
        public static string UploadImage(IFormFile imageFile, IWebHostEnvironment webHostEnvironment)
        {
            string uniqueFileName = null;

            if (imageFile != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    imageFile.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }

        public static bool IsImage(IFormFile imageFile)
        {
            if (imageFile == null) return false;

            var extension = Path.GetExtension(imageFile.FileName)?.ToLower();

            if (extension.Equals(".png") || extension.Equals(".jpg") || extension.Equals(".jpeg"))
            {
                return true;
            }

            return false;
        }
    }
}
