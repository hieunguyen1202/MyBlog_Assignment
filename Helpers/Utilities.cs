using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Helpers
{
    public static class Utilities
    {
        public static async Task<string> UploadFile(Microsoft.AspNetCore.Http.IFormFile file, string sDirectory, string newname = null)
        {
            if (newname == null)
                newname = file.FileName;

            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", sDirectory, newname);
            string path2 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", sDirectory);

            if (!Directory.Exists(path2))
            {
                Directory.CreateDirectory(path2);
            }

            var supportedTypes = new[] { "jpg", "jpeg", "png", "gif" };
            var fileExt = Path.GetExtension(file.FileName).Substring(1);

            if (!supportedTypes.Contains(fileExt.ToLower()))
            {
                // Return null for file types other than defined ones
                return null;
            }
            else
            {
                try
                {
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    return newname;
                }
                catch
                {
                    return null;
                }
            }
        }
        public static string GetRandomKey(int length = 5)
        {
            // Define the pattern for the random key
            string pattern = @"0123456789zxcvbnmasdfghjklqwertyuiop[]{}:~!@#$%^&*()+";

            Random rd = new Random();
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < length; i++)
            {
                sb.Append(pattern[rd.Next(0, pattern.Length)]);
            }

            return sb.ToString();
        }
    }
}
