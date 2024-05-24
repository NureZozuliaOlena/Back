using Microsoft.AspNetCore.Http;
using System.IO;

namespace Back.Interfaces
{
    public interface IDataRepository
    {
        public byte[] FileToBytes(IFormFile formFile);
    }
}
