using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp.Data
{
    public class UserRepository
    {
        private JsonAdapter _jsonAdapter;
        string path = HttpContext.Current.Server.MapPath("~/Json/") + "users.json";

        public UserRepository()
        {
            _jsonAdapter = new JsonAdapter(path);
        }

        public void Add(User user)
        {
            List<User> users = _jsonAdapter.Deserialize<User>();
            users.Add(user);
            _jsonAdapter.Serialize(users);
        }

        public bool Login(User user)
        {
            List<User> users = _jsonAdapter.Deserialize<User>();
            if (users.Find(x => x.Id == user.Id) != null)
            return true;
            return false;
        }

    }
}