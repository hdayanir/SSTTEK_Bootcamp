using Microsoft.AspNetCore.Mvc;
using WebApplication2.Context;

namespace WebApplication2.Domain
{
    public class CartItem
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Domain.Product Product { get; set; }
        public int Quantity { get; set; }
    }
}