using DongHo.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DongHo.DataAcess.IRepository
{
    public interface IBrandRepository : IRepository<Brand>
    {
        void Update(Brand brand);
    
    }
}
