using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BasicCRUD.Model;
using BasicCRUD.Service;

namespace BasicCRUD.Controllers
{
    public class UserController : Controller
    {
        private UserService userService = new UserService();

        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetUser()
        {
            return Json(userService.GetUserData());
        }

        [HttpPatch]
        public JsonResult UpdateUser(User user)
        {
            try
            {
                userService.UpdateUserData(user);
                return Json(true);
            }
            catch (Exception ex)
            {
                return Json(false);
            }
            
        }

        [HttpPost]
        public JsonResult CreateUser(User user)
        {
            try
            {
                return Json(userService.CreateUser(user));
                //return Json(true);
            }
            catch (Exception ex)
            {
                return Json(false);
            }
        }

        [HttpDelete]
        public JsonResult DeleteUser(string id)
        {
            try
            {
                userService.DeleteUser(id);
                return Json(true);
            }
            catch (Exception ex)
            {
                return Json(false);
            }
        }
    }
}