using AMS.Data;
using AMS.Models;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.EntityFrameworkCore;
using System;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace AMS.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly AmsContext _context;

        public StockRepository(AmsContext context)
        {
            _context = context;
        }

        public async Task<List<Stock>> GetStockListAsync(int branchId)
        {
            return await _context.Stocks
                .Include(s => s.Products)
                .Include(s => s.BranchMasters)
                .Where(s => branchId == 0 || s.BranchId == branchId)
                .ToListAsync();
        }

        public async Task<Stock> GetByProductAndBranchAsync(int productId, int branchId)
        {
            return await _context.Stocks
                .FirstOrDefaultAsync(s => s.ProductId == productId && s.BranchId == branchId);
        }

        public async Task AddAsync(Stock stock)
        {
            await _context.Stocks.AddAsync(stock);
            await _context.SaveChangesAsync();
        }
        public async Task AddAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Stock stock)
        {
            _context.Stocks.Update(stock);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Product>> GetProductAsync()
        {
            return await _context.Products.ToListAsync();
        }
        public IEnumerable<BranchMaster> GetBranches()
        {
            return _context.BranchMasters.ToList();
        }
        public IEnumerable<Product> GetProducts()
        {
            return _context.Products.ToList();
        }
        
    }


}
