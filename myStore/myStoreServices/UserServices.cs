
using myStore.DAL;
using myStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace myStore.myStoreServices
{
    public class UserServices
    {
        #region Singleton
        public static UserServices Instance
        {
            get
            {
                if (instance == null) instance = new UserServices();

                return instance;
            }
        }
        private static UserServices instance { get; set; }
        private UserServices() { }
        #endregion


        public List<User> GetUsers()
        {
            using (var context = new StoreContext())
            {

                return context.Users.ToList();
            }
        }
        public void SaveUser(User user)
        {
            using (var context = new StoreContext())
            {
                
                context.Users.Add(user);
                context.SaveChanges();
            }
        }

        public User GetUser(User user)
        {
            using (var context = new StoreContext())
            {

                return context.Users.Where(u => u.Username == user.Username && u.Password == user.Password).FirstOrDefault();
            }
        }
    }
}