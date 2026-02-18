using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EvatraUaeController : ControllerBase
    {
        private readonly IInvoiceService _invoiceService;
        public EvatraUaeController(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        [HttpGet("test-external")]
        public async Task<IActionResult> Test()
        {
            var result = await _invoiceService.GetInvoiceTraking();
            return Ok(result);
        }
    }
}
