using AMS.Enums;
using System.ComponentModel.DataAnnotations;

namespace AMS.Data
{
    public class Product
    {
        [Key]

        public int ProductId { get; set; }

        public string ProductName { get; set; }     
        public ProductType ProductType { get; set; }

        public string SKU { get; set; }
        public decimal Price { get; set; }

        public int CategoryId { get; set; }
        public Category Categories { get; set; }

        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; } = true;

        public ICollection<Stock> Stocks { get; set; }

    }
}

