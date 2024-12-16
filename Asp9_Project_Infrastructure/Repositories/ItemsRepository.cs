using Asp9_Project_Core.DTO_s;
using Asp9_Project_Core.Interfaces;
using Asp9_Project_Core.Mapping_Profiles;
using Asp9_Project_Core.Models;
using Asp9_Project_Infrastructure.Data;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asp9_Project_Infrastructure.Repositories
{
    public class ItemsRepository : IItemsRepository
    {
        private readonly AppDbContext appDbContext;

        public ItemsRepository( AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        //public async Task<IEnumerable<ItemsDTO>> GetItemsAsync()
        //{
        //    var items = await appDbContext.Items
        //        .Include(x=>x.ItemsUnits)
        //        .Select(x=> new ItemsDTO
        //        {
        //            Id = x.Id,
        //            Name = x.Name,
        //            price = x.price,
        //            ItemUnits = x.ItemsUnits.Select(unit => unit.Units.Name).ToList(),
        //            Stores = x.InvItemStores.Select(store => store.Stores.Name).ToList(),
        //        })
        //        .ToListAsync();
        //    return items;
        //}


        public async Task<PagedResponse<ItemsDTO>> GetItemsAsync(int page_index , int page_size)
        {
            var config = Mapping_Profile.Config;
            var items =  appDbContext.Items
                                    .ProjectToType<ItemsDTO>(config)
                                    .AsQueryable();
            var result =await PaginationAsync(items, page_index, page_size);
            return result;
        }

      

        public async Task<PagedResponse<ItemsDTO>> PaginationAsync(IQueryable<ItemsDTO> query, int page_index, int page_size)
        {
            var total_items = await query.CountAsync();

            var items = await query
                .Skip((page_index - 1) * page_size)   // page-index = 3-1= 2  ----- page_size = 5*2 =  10
                .Take(page_size)
                .ToListAsync();

            var result =  new PagedResponse<ItemsDTO>
            {
                total_items = total_items,
                items = items,
                page_index = page_index,
                page_size = page_size
            };
            return result;

        }
    }
}
