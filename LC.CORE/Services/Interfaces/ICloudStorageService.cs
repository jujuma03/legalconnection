using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace LC.CORE.Services
{
    public interface ICloudStorageService
    {
        Task<string> UploadFile(Stream stream, string cloudStorageContainer, string fileName, string extension);
        Task<bool> TryDelete(string fileName, string cloudStorageContainer);
        Task<Stream> Download(Stream stream, string fileName);
    }
}
