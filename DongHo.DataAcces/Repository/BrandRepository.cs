
using DongHo.DataAcces.Data;
using DongHo.DataAcess.IRepository;
using DongHo.Model;

namespace WebDongHo.DataAcess.Repository
{
    public class BrandRepository : Repository<Brand>, IBrandRepository
    {

        private readonly ApplicationDbContext _db;
        public BrandRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Save()
        {
            _db.SaveChanges();
        }    

        public void Update(Brand brand)
        {
            _db.Brands.Update(brand);
        }
    }
}
