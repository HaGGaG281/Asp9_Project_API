using Asp9_Project_Core.DTO_s;
using Asp9_Project_Core.Interfaces;
using Asp9_Project_Core.Models;
using Asp9_Project_Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asp9_Project_Infrastructure.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly AppDbContext appDbContext;

        public CartRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public async Task<string> AddBulkQuantityToCartAsync(CartItemDTO DTO, int? userId)
        {
            var items = await appDbContext.Items.FindAsync(DTO.ItemCode);
            var stores = await appDbContext.Stores.FindAsync(DTO.storeId);

            if(items == null || stores == null)
            {
                return "item or store not found";
            }

            var existingItem = appDbContext.ShoppingCartItems
                .FirstOrDefault(x=>x.Cus_Id == userId && x.Item_Id == DTO.ItemCode && x.Store_Id == DTO.storeId);

            if(existingItem!= null)
            {
                existingItem.Quantity = DTO.Quantity;
                existingItem.Unit_Id = DTO.UnitCode;
                existingItem.Store_Id = DTO.storeId;
                existingItem.UpdatedAt = DateTime.Now;
            }
            else
            {
                var shoppingCartItem = new ShoppingCartItems
                {
                   Cus_Id = userId,
                   Item_Id = DTO.ItemCode,
                   CreatedAt = DateTime.Now,
                   Quantity = DTO.Quantity,
                   Unit_Id = DTO.UnitCode,
                   Store_Id = DTO.storeId,
                   UpdatedAt = null,
                };
                appDbContext.Add(shoppingCartItem);
            }
            await appDbContext.SaveChangesAsync();
            return "Item added to cart successfully";

        }

      

        public async Task<string> AddOneQuantityToCartAsync(CartItemDTO cartItemDTO, int? userId)
        {
            var item = await appDbContext.Items.FindAsync(cartItemDTO.ItemCode);
            var store = await appDbContext.Stores.FindAsync(cartItemDTO.storeId);

            if (item == null || store == null)
            {
                return "item or store not found";
            }

            var existingItem = await appDbContext.ShoppingCartItems
                .FirstOrDefaultAsync(c=>c.Cus_Id == userId && c.Item_Id == cartItemDTO.ItemCode && c.Store_Id == cartItemDTO.storeId );
            
            if (existingItem != null)
            {
                existingItem.Quantity += 1;
                existingItem.UpdatedAt= DateTime.Now;
            }
            else
            {
                var shoppingcartitem = new ShoppingCartItems
                {
                    Cus_Id = userId,
                    Item_Id = cartItemDTO.ItemCode,
                    Store_Id = cartItemDTO.storeId,
                    Quantity = 1,
                    UpdatedAt = null,
                    CreatedAt = DateTime.Now,
                    Unit_Id = cartItemDTO.UnitCode
                };
                await appDbContext.ShoppingCartItems.AddAsync(shoppingcartitem);
            }
            await appDbContext.SaveChangesAsync();
            return "Item added to cart successfully";
        }

        public async Task<IEnumerable<UserCartItemsDTO>> getAllItemsFromCart(int? customerId)
        {
            var cartItems = await appDbContext.ShoppingCartItems.Where(x=>x.Cus_Id == customerId)
                            .Include(x=>x.Items)
                            .Include(x=>x.Items.ItemsUnits)
                            .ThenInclude(x=>x.Units)
                            .ToListAsync();
            var itemDto = cartItems.Select(x => new UserCartItemsDTO
            {
                name = x.Items.Name,
                price = x.Items.price,
                ItemUnit = x.Items.ItemsUnits
                           .Where(unit => unit.Unit_Id == x.Unit_Id && x.Item_Id == unit.Item_Id)
                           .Select(unit => unit.Units.Name)
                           .FirstOrDefault(),
                Quantity = x.Quantity

            }).ToList();
            return itemDto;
        }
    }
}
