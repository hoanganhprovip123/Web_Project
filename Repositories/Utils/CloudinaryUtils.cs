using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Newtonsoft.Json.Linq;

namespace WEB.Repositories.Utils
{
    public static class CloudinaryUtils
    { 
        public static readonly Account Account = new Account(
            Configs.CLOUDINARY_NAME,
            Configs.CLOUDINARY_APP_KEY,
            Configs.CLOUDINARY_APP_SECRET);
        public static Cloudinary Cloudinary { get; }
        static CloudinaryUtils()
        {
            Cloudinary = new Cloudinary(Account);
            Cloudinary.Api.Secure = true;
        }
        public static JToken UploadImage(IFormFile file)
        {
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(file.Name, file.OpenReadStream())
            };
            var uploadResult = Cloudinary.Upload(uploadParams);

            return uploadResult.JsonObj;
        }
        public static void DeleteImage(String public_id)
        {
            Cloudinary.DeleteResources(public_id);
        }
    }

}
