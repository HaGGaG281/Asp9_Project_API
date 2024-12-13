using Asp9_Project_Core.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asp9_Project_Core.Interfaces
{
    public interface IInvoiceRepository
    {
       Task<string> CreateInvoiceAsync(int customer_id);
       Task<InvoiceRecieptDTO> GetInvoiceReciept(int customer_id, int invoice_id);
    }
}
