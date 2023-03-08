
using DongHo.DataAcces.Data;
using DongHo.DataAcess.IRepository;
using DongHo.Model;
using System.Linq;

namespace WebDongHo.DataAcess.Repository
{
    public class OrderHeaderRepository : Repository<OrderHeader>, IOrderHeaderRepository
    {

        private readonly ApplicationDbContext _db;
        public OrderHeaderRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Save()
        {
            _db.SaveChanges();
        }    

      

        public void Update(OrderHeader orderHeader)
        {
            _db.OrderHeaders.Update(orderHeader);
        }

        public void UpdateStatus(int id, string orderStatus, string paymentStatus = null)
        {
            var orderFromDb = _db.OrderHeaders.FirstOrDefault(a => a.Id == id);
            if(orderFromDb !=null)
            {
                orderFromDb.OrderStatus = orderStatus;
                if(paymentStatus !=null)
                {
                    orderFromDb.PaymentStatus = paymentStatus;
                }
            }
        }
    }
}
