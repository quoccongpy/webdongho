using DongHo.Model;
using System;
using System.Collections.Generic;
using System.Text;


namespace DongHo.DataAcess.IRepository
{
    public interface ICategoryRepository : IRepository<Category>
    {
        void Update(Category category);
    
    }
}
