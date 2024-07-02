using Microsoft.AspNetCore.Http;

namespace _0_Framework.Application
{
    public interface IFileUploader
    {
        string UploadFile(IFormFile file,string path);
    }
}
