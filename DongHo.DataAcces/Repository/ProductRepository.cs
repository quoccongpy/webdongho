using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DongHo.DataAcces.Data;
using DongHo.DataAcess.IRepository;
using DongHo.Model;

namespace WebDongHo.DataAcess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {

        private readonly ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Save()
        {
            _db.SaveChanges();
        }    

        public void Update(Product product)
        {
            var data = _db.Products.SingleOrDefault(a => a.Id == product.Id);
            if(data!=null)
            {
                data.Name = product.Name;
                data.Description = product.Description;             
                data.Price = product.Price;
                data.Price5 = product.Price5;
                data.Price10 = product.Price10;
                data.CategoryId = product.CategoryId;
                data.CoverTypeId = product.CoverTypeId;
                data.BrandId = product.BrandId;
                if(product.ImageUrl!=null)
                {
                    data.ImageUrl = product.ImageUrl;
                }
            }
            _db.Products.Update(data);
        }
    }
}
