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

        //public async Task<List<Stock>> GetStocksByBranchAsync(int branchId )
        //{
        //    return await _context.Stocks
        //        //.Include(s => s.Products) 
        //        //.Where(s => s.BranchId == branchId)
        //        .ToListAsync();
        //}
        public List<StockModel> GetStockList(int? branchId = null)
        {
            var query = _context.Stocks
                .Include(p => p.BranchMasters)
                .Include(p => p.Products)
                .AsQueryable();

            if (branchId != null && branchId > 0)
            {
                query = query.Where(p => p.BranchId == branchId.Value);
            }

            return query.Select(p => new StockModel
            {
                BranchId = p.BranchId,
                BranchName = p.BranchMasters.BranchName,
                ProductId = p.ProductId,
                ProductName = p.Products.ProductName,
                Quantity = p.Quantity,
                LastUpdated = p.LastUpdated
            }).ToList();
        }


    }
}
