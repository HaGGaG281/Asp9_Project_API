using Asp9_Project_Core.DTO_s;
using Asp9_Project_Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Printing;

namespace Asp9_Project_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        //private readonly IItemsRepository itemsRepository;

        public ItemsController(/*IItemsRepository itemsRepository*/ IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            //this.itemsRepository = itemsRepository;
        }

        [HttpGet("Items")]
        public async Task<ActionResult<IEnumerable<ItemsDTO>>> GetItems( int page_size=5 , int page_index =1)
        {
            var items = await unitOfWork.ItemsRepository.GetItemsAsync(page_index , page_size);
            if(items == null)
            {
                return NotFound("items not exists");
            }
            return Ok(items);
        }
    }
}
