using DongHo.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DongHo.DataAcess.IRepository
{
    public interface IShoppingCartRepository : IRepository<ShoppingCart>
    {
        int PlusCount(ShoppingCart shoppingCart, int count);
        int MinusCount(ShoppingCart shoppingCart, int count);

    }
}
