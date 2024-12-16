using Asp9_Project_Core.Interfaces;
using Asp9_Project_Core.Models;
using Asp9_Project_Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asp9_Project_Infrastructure.Repositories
{
    public class UnitOfWOrk : IUnitOfWork , IDisposable
    {
        private readonly AppDbContext appContext;

        public IAuthRepository AuthRepository { get; }

        public ICartRepository CartRepository { get; }

        public IInvoiceRepository InvoiceRepository { get; }

        public IItemsRepository ItemsRepository { get; }


        public UnitOfWOrk(AppDbContext appContext , UserManager<Users> userManager , SignInManager<Users> signInManager , IConfiguration configuration)
        {
            this.appContext = appContext;
            AuthRepository = new AuthRepository( userManager, signInManager, configuration);
            CartRepository = new CartRepository(appContext);
            InvoiceRepository = new InvoiceRepository(appContext);
            ItemsRepository = new ItemsRepository(appContext);

        }

        public async Task<int> saveAsync()
         => await appContext.SaveChangesAsync();

        public void Dispose()
        {
            appContext.Dispose();
        }
    }
}
