using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WsGetCludrByAddress.WebService;

namespace WsGetCludrByAddress.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class Controller : ControllerBase {
        private readonly Service _service;

        public Controller(Service service) =>
            _service = service;
        

        [HttpGet("GetDaDataInfoByAddress")]
        public IActionResult Get(string address) {
            var result = _service.GetInfoAsync(address);
            return Ok(result);
        }
    }
}
