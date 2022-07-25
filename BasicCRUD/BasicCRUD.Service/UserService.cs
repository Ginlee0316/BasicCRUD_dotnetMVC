using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasicCRUD.Dao;
using BasicCRUD.Model;

namespace BasicCRUD.Service
{
    public  class UserService
    {
        private UserDao userDao = new UserDao();

        public int CreateUser(User user)
        {
            return userDao.CreateUser(user);
        }

        public List<User> GetUserData()
        {
            List<User> users = new List<User>();

            users = userDao.GetUserData();

            return users;
        }

        public User GetUserById(string userId)
        {
            User user = new User();

            user = userDao.GetUserById(userId);

            return user;
        }

        public void UpdateUserData(User user)
        {
            userDao.UpdateUserData(user);
        }

        public void DeleteUser(string userId)
        {
            userDao.DeleteUser(userId);
        }
    }
}
