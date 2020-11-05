using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

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

        [HttpGet("read-qr-code")]
        public async Task<string> ReadQrCode() {
            var apiUrl = configuration["QRReaderApiUrl"];
            using var content = GetContentFile();
            var response = await httpClient.PostAsync(apiUrl, content);
            var text = await response.Content.ReadAsStringAsync();

            return text;
        }

        private HttpContent GetContentFile() {
            var filePath = $"{AppDomain.CurrentDomain.BaseDirectory}images/qrcode.jpeg";
            var bytes = System.IO.File.ReadAllBytes(filePath);
            var arrayContent = new ByteArrayContent(bytes);
            var content = new MultipartFormDataContent() {
                { arrayContent, "file", "qrcode.jpeg"}
            };
            
            return content;
        }
    }
}