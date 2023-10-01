using Microsoft.AspNetCore.Mvc;
using WebApplication2.Context;

namespace WebApplication2.Domain
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public bool IsPublished { get; set; }
        public int CategoryId { get; set; }
        public Domain.Category Category { get; set; }
    }
}