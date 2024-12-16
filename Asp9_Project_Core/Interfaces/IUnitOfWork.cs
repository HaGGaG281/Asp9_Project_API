using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asp9_Project_Core.Interfaces
{
    public interface IUnitOfWork
    {
        IAuthRepository AuthRepository { get; }
        ICartRepository CartRepository { get; }
        IInvoiceRepository InvoiceRepository { get; }
        IItemsRepository ItemsRepository { get; }
        Task<int> saveAsync();

    }
}
