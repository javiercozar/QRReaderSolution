using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
            var content = GetContentFile();
            var response = await httpClient.PostAsync(apiUrl, content);

            return "";
        }

        private HttpContent GetContentFile() {
            var filePath = $"{AppDomain.CurrentDomain.BaseDirectory}/images/qrvode.jpeg";

            throw new NotImplementedException();
        }
    }
}