using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asp9_Project_Core.DTO_s
{
    public class InvoiceItemsDTO
    {
        public string item_name { get; set; }
        public double quantity { get; set; }
        public string unit_name { get; set; }
        public double price_per_unit { get; set; }
        public double total_price { get; set; }
    }
}
