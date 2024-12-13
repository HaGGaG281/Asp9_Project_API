using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asp9_Project_Core.DTO_s
{
    public class InvoiceRecieptDTO
    {
        public int invoice_id { get; set; }
        public int customer_id { get; set; }
        public double total_price { get; set; }
        public DateTime created_at { get; set; }
        public List<InvoiceItemsDTO> items { get; set; }
    }
}
