using Back.Interfaces;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace Back.Repositories
{
    public class DataRepository : IDataRepository
    {
        public byte[] FileToBytes(IFormFile formFile)
        {
            byte[] imageData = null;
            using (var binaryReader = new BinaryReader(formFile.OpenReadStream()))
            {
                imageData = binaryReader.ReadBytes((int)formFile.Length);
            }

            return imageData;
        }
    }
}
