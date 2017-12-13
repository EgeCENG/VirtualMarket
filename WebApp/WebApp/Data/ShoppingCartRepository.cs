using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp.Data
{
    public class ShoppingCartRepository
    {
        private JsonAdapter _jsonAdapter;
        private SaleRepository _saleRepository;
        private string path = HttpContext.Current.Server.MapPath("~/Json/") + "shoppingcart.json";

        public ShoppingCartRepository()
        {
            _jsonAdapter = new JsonAdapter(path);
            _saleRepository = new SaleRepository();
        }

        public void Add(ShoppingCart cart)
        {
            List<ShoppingCart> shoppingCarts = GetAll();
            cart.Id = Guid.NewGuid().ToString();
            shoppingCarts.Add(cart);
            _jsonAdapter.Serialize(shoppingCarts);
        }

        public List<ShoppingCart> GetAll()
        {
            return _jsonAdapter.Deserialize<ShoppingCart>();
        }
    }
}