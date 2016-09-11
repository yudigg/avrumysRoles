using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DataStuff;

namespace MvcApplication38.Controllers
{


    public class HomeController : Controller
    {
        public ActionResult Index()
        {

          var all =  Roles.GetAllRoles();
            return View(all);
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            var repo = new Repo(Properties.Settings.Default.ConStr);
            var user = repo.Login(username, password);
            if (user == null)
            {
                return Redirect("/home/index");
            }
            FormsAuthentication.SetAuthCookie(username, true);
            //var authTicket = new FormsAuthenticationTicket(1, //version
            //           username,
            //           DateTime.Now,             //creation
            //           DateTime.Now.AddMinutes(30), //Expiration
            //           true, user.Id.ToString());
            //var encTicket = FormsAuthentication.Encrypt(authTicket);
            //Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));
            return Redirect("/secret/index");
            // return new RedirectResult("/secret/index");
        }

        public ActionResult Signup()
        {
            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return Redirect("/");
        }

        [HttpPost]
        public ActionResult Signup(string name, string username, string password)
        {
            var user = new User { Name = name, Username = username, PasswordHash = FakeHasher.Hash(password) };
            var repo = new Repo(Properties.Settings.Default.ConStr);
            repo.AddUser(user);
          var role =  repo.CreateRole("super");
            repo.AddUserToRole(user.Id,role.Id);
            return Redirect("/home/index");
        }
    }


}
