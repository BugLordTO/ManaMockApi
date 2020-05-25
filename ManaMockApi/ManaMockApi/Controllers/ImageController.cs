using ManaMockApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ManaMockApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ImageController : ControllerBase
    {
        private readonly CloudBlobClient cloudBlobClient;
        private readonly HttpClient httpClient = new HttpClient();

        private string StorageAccountConnectionString = "DefaultEndpointsProtocol=https;AccountName=manadevfrom;AccountKey=OVAjcObPc/JaKCUECEyZomzu1pLoZzkES152gyFWTNie66YGPSRf03Uf85bYKkgC43RxheMFHoxV3APCEG4KiA==;EndpointSuffix=core.windows.net";
        private string StorageAccountBaseUrl = "https://manadevfrom.blob.core.windows.net";
        private string ProductContainerName = "productimages";

        public ImageController()
        {
            if (CloudStorageAccount.TryParse(StorageAccountConnectionString, out CloudStorageAccount StorageAccount))
            {
                cloudBlobClient = StorageAccount.CreateCloudBlobClient();
            }
        }

        /// <summary>
        /// Get Upload Image Blob SaS
        /// </summary>
        /// <param name="type"></param>
        /// <param name="refid"></param>
        /// <param name="serviceId"></param>
        /// <param name="bizAccountId"></param>
        /// <returns></returns>
        [HttpGet("sas")]
        public async Task<ActionResult<ImageBlobSaS>> GetUploadImageBlobSaS(string type, string refid, string serviceId, string bizAccountId)
        {
            var now = DateTimeOffset.UtcNow;
            var imageId = Guid.NewGuid().ToString();
            var cloudBlobContainer = cloudBlobClient.GetContainerReference(ProductContainerName);
            await cloudBlobContainer.CreateIfNotExistsAsync();
            var cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(imageId);
            var sas = cloudBlockBlob.GetSharedAccessSignature(new SharedAccessBlobPolicy()
            {
                Permissions = SharedAccessBlobPermissions.Write,
                SharedAccessStartTime = now.AddMinutes(-5),
                SharedAccessExpiryTime = now.AddMinutes(30),
            });

            return Ok(new ImageBlobSaS
            {
                StorageUri = StorageAccountBaseUrl,
                ContainerName = ProductContainerName,
                ImageId = imageId,
                SaS = sas,
            });
        }

        /// <summary>
        /// Get Image Content
        /// </summary>
        /// <param name="imageId"></param>
        /// <returns></returns>
        [HttpGet("{imageId}")]
        public async Task<IActionResult> Image(string imageId)
        {
            var now = DateTimeOffset.UtcNow;
            var cloudBlobContainer = cloudBlobClient.GetContainerReference(ProductContainerName);
            await cloudBlobContainer.CreateIfNotExistsAsync();
            var cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(imageId);
            if (!await cloudBlockBlob.ExistsAsync())
            {
                return NotFound(new { message = "Image not found." });
            }

            var sas = cloudBlockBlob.GetSharedAccessSignature(new SharedAccessBlobPolicy()
            {
                Permissions = SharedAccessBlobPermissions.Read,
                SharedAccessStartTime = now.AddMinutes(-5),
                SharedAccessExpiryTime = now.AddMinutes(30),
            });

            var blobUrl = $"{StorageAccountBaseUrl}/{ProductContainerName}/{imageId}{sas}";
            return Redirect(blobUrl);
            //var response = await httpClient.GetAsync(blobUrl);
            //var mediaType = response.Content.Headers.ContentType.MediaType;
            //using (var stream = await response.Content.ReadAsStreamAsync())
            //{
            //    return File(stream, mediaType);
            //}
        }

        //[HttpGet("asdasd")]
        //public async Task<IActionResult> Upload(string sas)
        //{
        //    var cloudBlockBlob = new CloudBlockBlob(new Uri(sas));
        //    await cloudBlockBlob.UploadFromFileAsync(@"C:\Users\Aftershock\Desktop\QrPay-Event\sc.png");
        //    return Ok();
        //}
    }
}