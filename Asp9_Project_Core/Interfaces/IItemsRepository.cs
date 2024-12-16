using Asp9_Project_Core.DTO_s;
using Asp9_Project_Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asp9_Project_Core.Interfaces
{
    public interface IItemsRepository
    {
        Task<PagedResponse<ItemsDTO>> GetItemsAsync(int page_index, int page_size);
        Task<PagedResponse<ItemsDTO>> PaginationAsync(IQueryable<ItemsDTO> query, int page_index, int pagesize);
    }
}
