using Asp9_Project_Core.DTO_s;
using Asp9_Project_Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Asp9_Project_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IItemsRepository itemsRepository;

        public ItemsController(IItemsRepository itemsRepository)
        {
            this.itemsRepository = itemsRepository;
        }

        [HttpGet("Items")]
        public async Task<ActionResult<IEnumerable<ItemsDTO>>> GetItems()
        {
            var items = await itemsRepository.GetItemsAsync();
            if(items == null)
            {
                return NotFound("items not exists");
            }
            return Ok(items);
        }
    }
}
