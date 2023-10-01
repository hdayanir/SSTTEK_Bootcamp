using Microsoft.AspNetCore.Mvc;
using WebApplication2.Context;

namespace WebApplication2.Domain
{
    public class Cart
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public Domain.User User { get; set; }
        public List<Domain.CartItem> CartItems { get; set; }
    }
}