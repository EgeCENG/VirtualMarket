using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp.Data
{
    public class SaleRepository
    {
        private JsonAdapter _jsonAdapter;
        private string path = HttpContext.Current.Server.MapPath("~/Json/") + "sales.json";

        public SaleRepository()
        {
            _jsonAdapter = new JsonAdapter(path);
        }

        public void Add(Sale sale)
        {
            List<Sale> sales = _jsonAdapter.Deserialize<Sale>();
            sale.Id = Guid.NewGuid().ToString();
            sales.Add(sale);
            _jsonAdapter.Serialize(sales);
        }

        public List<Sale> GetAll()
        {
            return _jsonAdapter.Deserialize<Sale>();
        }

        public Sale GetByUser(User user)
        {
            return GetAll().Find(x => x.User.Id == user.Id);
        } 
    }
}