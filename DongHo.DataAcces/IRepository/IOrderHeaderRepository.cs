using DongHo.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DongHo.DataAcess.IRepository
{
    public interface IOrderHeaderRepository : IRepository<OrderHeader>
    {
        void Update(OrderHeader orderHeader);

        void UpdateStatus(int id, string orderStatus, string? paymentStatus = null);
        //void UpdateStripePaymentID(int id, string sessionId, string paymentItentId);

    }
}
