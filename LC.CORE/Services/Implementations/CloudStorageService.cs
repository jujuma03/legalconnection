using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Threading.Tasks;
using LC.CORE.Helpers;
using System.Linq;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using LC.CORE.Extensions;
using LC.CORE.Services.Models;
using Microsoft.Azure.Storage.Blob;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Auth;

namespace LC.CORE.Services
{
    public class CloudStorageService : ICloudStorageService
    {
        public async Task<string> UploadFile(Stream stream, string cloudStorageContainer, string fileName, string extension)
        {
            var yearFolder = DateTime.Today.Year.ToString();
            var projectFolderPath = Path.Combine(ConstantHelpers.GENERAL.FILESTORAGE.PATH, yearFolder, cloudStorageContainer);

            var relativePath = Path.Combine(yearFolder, cloudStorageContainer);

            bool exists = Directory.Exists(projectFolderPath);

            if (!exists)
                Directory.CreateDirectory(projectFolderPath);

            var filePath = Path.Combine(projectFolderPath, $"{fileName}{extension}");
            relativePath = Path.Combine(relativePath, $"{fileName}{extension}");

            using (var newstream = new FileStream(filePath, FileMode.Create))
            {
                await stream.CopyToAsync(newstream);
            }
            return relativePath;
        }

        public async Task<bool> TryDelete(string fileName, string cloudStorageContainer)
        {
            var filePath = Path.Combine(ConstantHelpers.GENERAL.FILESTORAGE.PATH, fileName);
            if (File.Exists(filePath)) File.Delete(filePath);
            return true;
        }

        public async Task<Stream> Download(Stream stream, string fileName)
        {
            var filePath = Path.Combine(ConstantHelpers.GENERAL.FILESTORAGE.PATH, fileName);
            if (File.Exists(filePath))
            {
                using (var file = new FileStream(filePath, FileMode.Open))
                {
                    await file.CopyToAsync(stream);
                }
                return stream;
            }

            return null;
        }
    }
}