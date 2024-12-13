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


        public async Task<IEnumerable<ItemsDTO>> GetItemsAsync()
        {
            var config = Mapping_Profile.Config;
            var items = await appDbContext.Items
                                    .ProjectToType<ItemsDTO>(config)
                                    .ToListAsync();
            return items;
        }
    }
}
