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
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly AppDbContext appDbContext;

        public InvoiceRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public async Task<string> CreateInvoiceAsync(int customer_id)
        {
            var cartItems = await appDbContext.ShoppingCartItems
                .Include(c=>c.Items)
                .Where(c=>c.Cus_Id == customer_id)
                .ToListAsync();

            if(cartItems == null || !cartItems.Any())
            {
                return "No items in the cart to create invoice";
            }

            var unavailableitems = new List<string>();
            double TotalNetPrice = 0;

            foreach (var item in cartItems)
            {
                var itemstore = appDbContext.InvItemStores
                    .FirstOrDefault(i => i.Item_Id == item.Item_Id && i.Store_Id == item.Store_Id);

                if (itemstore == null)
                {
                    unavailableitems.Add(item.Items.Name);
                    continue;
                }

                double availablequantity = itemstore.Balance - itemstore.ReservedQuantity;

                if (item.Quantity > availablequantity)
                {
                    unavailableitems.Add(item.Items.Name);
                    continue;
                }
            }
            var unavailableitemsCount = unavailableitems.Count();
            var numberOfCartItems = cartItems.Count();
            if (unavailableitemsCount == numberOfCartItems)
            {
                return "All items in cart UnAvailable :(";
            }

            var invoice = new Invoice
            {
                Cus_Id = customer_id,
                CreatedAt = DateTime.Now,
                NetPrice = 0,
                Transaction_Type = 1,
                Payment_Type = 1,
                isPosted = true,
                isClosed = false,
                isReviewed = false,
            };

            await appDbContext.Invoice.AddAsync(invoice);
            await appDbContext.SaveChangesAsync();




            foreach (var item in cartItems)
            {

                var itemstore = appDbContext.InvItemStores
                                 .FirstOrDefault(i => i.Item_Id == item.Item_Id && i.Store_Id == item.Store_Id);
                double unitprice = item.Items.price;
                double ItemTotalPrice = item.Quantity * unitprice; 
                TotalNetPrice += ItemTotalPrice;


                var invoiceDetail = new InvoiceDetails
                {
                    Invoice_Id = invoice.Id,
                    Item_Id = item.Item_Id,
                    Quantity = item.Quantity,
                    Factor = 1,
                    price = (int)unitprice,
                    Unit_Id = item.Unit_Id,
                    CreatedAt = DateTime.Now,
                };
                appDbContext.InvoiceDetails.Add(invoiceDetail);

                itemstore.ReservedQuantity += item.Quantity;
                appDbContext.InvItemStores.Update(itemstore);

            }

            invoice.NetPrice = TotalNetPrice;

            appDbContext.ShoppingCartItems.RemoveRange(
                cartItems.Where(i=> !unavailableitems.Contains(i.Items.Name))
                );
            await appDbContext.SaveChangesAsync();

            if (unavailableitems.Any())
            {
                var unavailableItemsMessage = string.Join(", ", unavailableitems.Select(item =>
                {
                    var cartItem = cartItems.FirstOrDefault(i => i.Items.Name == item);
                    if(cartItem != null)
                    {
                        var itemStore = appDbContext.InvItemStores
                        .FirstOrDefault(i => i.Item_Id == cartItem.Item_Id);
                        
                        if(itemStore != null)
                        {
                            double availableQuantity = itemStore.Balance - itemStore.ReservedQuantity;
                            return $"{item} (Available Quantity = {availableQuantity})";
                        }
                    }
                    return item;
                }));

                return $"invoice created successfully with ID: {invoice.Id} and total price : {TotalNetPrice} , However the following items were unavailable {unavailableItemsMessage} ";
            }
            return $"invoice created successfully with ID: {invoice.Id} and total price : {TotalNetPrice}";


        }

        public Task<InvoiceRecieptDTO> GetInvoiceReciept(int customer_id, int invoice_id)
        {
            throw new NotImplementedException();
        }
    }
}
