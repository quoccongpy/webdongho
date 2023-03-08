using System;
using System.Collections.Generic;
using System.Text;
using DongHo.Model;

namespace DongHo.DataAcess.IRepository
{
    public interface IProductRepository : IRepository<Product>
    {
        void Update(Product product);
    
    }
}
