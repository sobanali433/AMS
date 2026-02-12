using AMS.Data;
using Microsoft.EntityFrameworkCore;

namespace AMS.Repository
{
    public interface IProductRepository
    {
        //List<Product> GetAll();
        List<Product> GetList();
        
        List<Category> GetCategories();
        
        Task<bool> AddproductAsync(Product model);
        Task<Product?> GetByIdAsync(int id);
        Task<bool> UpdateAsync(Product product);

    }
}

