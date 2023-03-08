using DongHo.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DongHo.DataAcess.IRepository
{
    public interface IOrderDetailRepository : IRepository<OrderDetail>
    {
        void Update(OrderDetail orderDetail);
    
    }
}
