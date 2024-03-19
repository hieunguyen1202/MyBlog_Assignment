using Minio.DataModel;
using Minio.Exceptions;
using Minio;
using System;
using System.Threading.Tasks;
using System.Security.Policy;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace MyBlog.Models
{

    public static class MinIO
    {
        public static async Task PutMinIO(IFormFile file)
        {
            try
            {
                // Initialize MinIO client
                var accessKey = "kztek";
                var secretKey = "Kztek123456";
                var minio = new MinioClient()
                .WithEndpoint($"14.160.26.45:29080")
                .WithCredentials(accessKey, secretKey)
                .WithRegion("us-west-rack")
                .WithSSL(false) // Set to true if MinIO server uses SSL/TLS
                .Build();

                Console.WriteLine("File example:");

                // Specify bucket name, object name, file name, and content type
                var bucketName = "myblog"; // Replace with your bucket name
                var objectName = file.FileName;
                var contentType = file.ContentType;
                var stream = file.OpenReadStream();
                var size = file.Length;
                // Upload object to MinIO
                PutObjectArgs poa = new PutObjectArgs()
             .WithBucket(bucketName)
             .WithObject(objectName)
             .WithContentType(contentType)
                      .WithStreamData(stream).WithObjectSize(size);
                await minio.PutObjectAsync(poa);
                Console.WriteLine($"\nSuccessfully uploaded {objectName}\n");

            }
            catch (MinioException ex)
            {
                Console.WriteLine($"Error occurred: {ex.Message}");
            }
        }
        public static async Task<string> GetMinIO(string objName)
        {
            try
            {
                // Initialize MinIO client
                var accessKey = "kztek";
                var secretKey = "Kztek123456";
                var minio = new MinioClient()
                .WithEndpoint($"14.160.26.45:29080")
                .WithCredentials(accessKey, secretKey)
                .WithRegion("us-west-rack")
                .WithSSL(false) // Set to true if MinIO server uses SSL/TLS
                .Build();
                var bucketName = "myblog";
                var expiryInSeconds = 3600; // Expiry time in seconds


                PresignedGetObjectArgs args = new PresignedGetObjectArgs()
                                                  .WithBucket(bucketName)
                                                  .WithObject(objName)
                                                  .WithExpiry(expiryInSeconds);
                String url = await minio.PresignedGetObjectAsync(args);
                Console.WriteLine(url);
                return url;

            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR" + ex.ToString());
                return null;
            }
        }
    }
}
