namespace SportsStore.Models
{
    using System.Linq;
    using Microsoft.EntityFrameworkCore;



    public class EFOrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public EFOrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<Order> Orders =>
            _context
                .Orders
                .Include(x => x.Lines)
                .ThenInclude(x => x.Product);

        public void SaveOrder(Order order)
        {
            _context.AttachRange(order.Lines.Select(x => x.Product));

            if (order.OrderID == 0)
            {
                _context.Orders.Add(order);
            }

            _context.SaveChanges();
        }
    }
}