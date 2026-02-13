using AMS.Data;
using Microsoft.EntityFrameworkCore;

namespace AMS.Repository
{
    public interface IProductRepository
    {
        //List<Product> GetAll();
        List<object> GetProductList();
        List<Category> GetCategories();
        
        Task<bool> AddproductAsync(Product model);
        Task<Product?> GetByIdAsync(int id);
        Task<bool> UpdateAsync(Product product);

        (bool isSuccess, string message) ProductDelete(int id);

    }
}

