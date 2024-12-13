using Asp9_Project_Core.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asp9_Project_Core.Interfaces
{
    public interface ICartRepository
    {
       Task<string> AddBulkQuantityToCartAsync(CartItemDTO cartItemDTO, int? userId);
       Task<string> AddOneQuantityToCartAsync(CartItemDTO cartItemDTO, int? userId);

        Task<IEnumerable<UserCartItemsDTO>> getAllItemsFromCart(int? customerId);
    }
}
