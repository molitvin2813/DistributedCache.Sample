using Microsoft.EntityFrameworkCore;
using ProductService.Domain.Models;

namespace ProductService.Application.Interfaces
{
    public interface IProductServiceContext
    {
        DbSet<Product> Products { get; set; }

        Task<int> SaveChangesAsync(CancellationToken token);
    }
}
