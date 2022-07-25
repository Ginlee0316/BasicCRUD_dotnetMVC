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

            //users = userDao.GetUserData();

            users.Add(new User() { UserId = "1", UserAccount = "a001", UserName = "a001_Name", Email = "a001@xyz.com" });
            users.Add(new User() { UserId = "2", UserAccount = "a002", UserName = "a002_Name", Email = "a002@xyz.com" });
            users.Add(new User() { UserId = "3", UserAccount = "a003", UserName = "a003_Name", Email = "a003@xyz.com" });

            return users;
        }

        public User GetUserById(string userId)
        {
            User user = new User();

            //user = userDao.GetUserById(userId);

            user.UserId = "1";
            user.UserAccount = "a001";
            user.UserName = "a001_Name";
            user.Email = "a001@xyz.com";


            return user;
        }

        public void UpdateUserData(User user)
        {
            //userDao.UpdateUserData(user);
        }

        public void DeleteUser(string userId)
        {
            //userDao.DeleteUser(userId);
        }
    }
}
