using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace QRReader.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class QRController : ControllerBase {
        readonly HttpClient httpClient;
        private readonly IConfiguration configuration;
         
        public QRController(IConfiguration configuration) {
            httpClient = new HttpClient();
            this.configuration = configuration;
        }

        [HttpPost("read-qr-code")]
        public async Task<string> ReadQrCode(string pathFile) {
            var apiUrl = configuration["QRReaderApiUrl"];
            using var content = GetContentFile(pathFile);
            var response = await httpClient.PostAsync(apiUrl, content);
            var text = await response.Content.ReadAsStringAsync();

            return text;
        }

        private HttpContent GetContentFile(string pathFile) {
            var fileInfo = new FileInfo(pathFile);

            if (!fileInfo.Exists) {
                throw new Exception("File doesn't exist");
            }

            var bytes = System.IO.File.ReadAllBytes(pathFile);
            var arrayContent = new ByteArrayContent(bytes);
            var content = new MultipartFormDataContent() {
                { arrayContent, "file", "qrcode.jpeg"}
            };
            
            return content;
        }
    }
}