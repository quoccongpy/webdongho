using System;
using System.Collections.Generic;
using System.Text;
using DongHo.DataAcces.Data;
using DongHo.DataAcess.IRepository;
using DongHo.Model;

namespace WebDongHo.DataAcess.Repository
{
    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {

        private readonly ApplicationDbContext _db;
        public CompanyRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(Company company)
        {
            _db.Companies.Update(company);
        }
    }
}
