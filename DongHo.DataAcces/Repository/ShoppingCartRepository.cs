
using DongHo.DataAcces.Data;
using DongHo.DataAcess.IRepository;
using DongHo.Model;

namespace WebDongHo.DataAcess.Repository
{
    public class ShoppingCartRepository : Repository<ShoppingCart>, IShoppingCartRepository
    {

        private readonly ApplicationDbContext _db;
        public ShoppingCartRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        //public int MinusCount(ShoppingCart shoppingCart, int count)
        //{
        //    shoppingCart.Count -= count;
        //    return shoppingCart.Count;
        //}

        //public int PlusCount(ShoppingCart shoppingCart, int count)
        //{
        //    shoppingCart.Count += count;
        //    return shoppingCart.Count;
        //}

        public void Save()
        {
            _db.SaveChanges();
        }

        int IShoppingCartRepository.MinusCount(ShoppingCart shoppingCart, int count)
        {
            shoppingCart.Count -= count;
            return shoppingCart.Count;
        }

        int IShoppingCartRepository.PlusCount(ShoppingCart shoppingCart, int count)
        {
            shoppingCart.Count += count;
            return shoppingCart.Count;
        }
    }
}
