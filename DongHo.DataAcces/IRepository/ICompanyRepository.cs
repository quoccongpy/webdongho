using DongHo.Model;
using System;
using System.Collections.Generic;
using System.Text;


namespace DongHo.DataAcess.IRepository
{
    public interface ICompanyRepository : IRepository<Company>
    {
        void Update(Company company);
    
    }
}
