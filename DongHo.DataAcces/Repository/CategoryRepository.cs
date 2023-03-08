using System;
using System.Collections.Generic;
using System.Text;
using DongHo.DataAcces.Data;
using DongHo.DataAcess.IRepository;
using DongHo.Model;

namespace WebDongHo.DataAcess.Repository
{
    public class CategoryRepository: Repository<Category>, ICategoryRepository
    {

        private readonly ApplicationDbContext _db;
        public CategoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(Category category)
        {
            _db.Categories.Update(category);
        }
    }
}
