using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace CateringProcessor.Controllers
{
    [ApiController]
    [Route("Catering/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly ILogger<OrdersController> _logger;
        IConfiguration _configuration;

        public OrdersController(ILogger<OrdersController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        [HttpGet()]
        [Route("/")]
        public IActionResult Index()
        {
            string serverDateTime = DateTime.UtcNow.ToString("MM/dd/yyyy HH:mm:ss");
            var appNameServerDateTime = $"App name : Catering Processor, Server Date Time: {serverDateTime}  (UTC)";

            return Ok(appNameServerDateTime);
        }

        [HttpGet("Testing")]
        public ActionResult<string> Testing()
        {
            return Ok("Catering WebHook Test");
        }

        [HttpPost("OrderEvent")]
        public ActionResult OrderEvent()
        {
            _logger.LogInformation("Received Orders Event");

            bool displayHeaders = _configuration.GetValue<bool>("AppSettings:CateringLogHeaders");
            if (displayHeaders)
            {
                foreach (var header in Request.Headers)
                {
                    _logger.LogInformation($"Header : {header.Key}, Value : {header.Value}");
                }
            }

            return Ok("Order Event Received");
        }
    }
}
