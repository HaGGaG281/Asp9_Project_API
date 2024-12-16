using Asp9_Project_API.HelperFunctions;
using Asp9_Project_Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Asp9_Project_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoicesController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        /*private readonly IInvoiceRepository invoiceRepository ; */

        public InvoicesController( IUnitOfWork unitOfWork /*IInvoiceRepository invoiceRepository*/  )
        {
            this.unitOfWork = unitOfWork;
            //this.invoiceRepository = invoiceRepository;
        }


        [HttpPost("create")]
        public async Task<IActionResult> createInvoice()
        {
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized("token is missing");
            }

            
                var userId = ExtractClaims.EtractUserId(token);
                if (!userId.HasValue)
                {
                    return Unauthorized("invalid user token");
                }
                var result = await unitOfWork.InvoiceRepository.CreateInvoiceAsync(userId.Value);
                

                if (result.StartsWith("invoice created successfully"))
                {
                    return Ok(result);
                }
                return BadRequest(result);
        }


        [HttpGet("get")]
        public async Task<IActionResult> GetInvoiceReciept(int invoice_id)
        {
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized("token is missing");
            }


            var userId = ExtractClaims.EtractUserId(token);
            if (!userId.HasValue)
            {
                return Unauthorized("invalid user token");
            }

            var reciept =await unitOfWork.InvoiceRepository.GetInvoiceReciept(userId.Value, invoice_id);
            if(reciept == null)
            {
                return NotFound("Invoice not found ");
            }

            return Ok(reciept);


        }


    }
}
