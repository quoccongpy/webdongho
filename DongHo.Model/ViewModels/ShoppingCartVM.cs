using System;
using System.Collections.Generic;
using System.Text;

namespace DongHo.Model.ViewModels
{
    public class ShoppingCartVM
    {
        public IEnumerable<ShoppingCart> listCart { get; set; }
        public double cartTotal { get; set; }
        public OrderHeader OrderHeader { get; set; }
    }
}
