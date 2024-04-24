using Microsoft.AspNetCore.Http;

namespace MyResume.Infrasctructure
{
    public class FileConverter
    {
        public static byte[] ToBytes(IFormFile file)
        {
            byte[] bytes = null;

            using (var binaryReader = new BinaryReader(file.OpenReadStream()))
            {
                bytes = binaryReader.ReadBytes((int)file.Length);
            }

            return bytes;
        }

        public static IFormFile ToFile(byte[] bytes, string fileName)
        {
            MemoryStream stream = new MemoryStream(bytes);

            IFormFile file = new FormFile(stream,0,bytes.LongLength,"file", fileName);

            return file;
        }
    }
}
