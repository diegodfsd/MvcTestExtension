using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Site.Models;

namespace Site.Controllers
{
    public class UserController : Controller
    {
        private IList<User> users;

        public UserController()
        {
            users = new List<User>
                        {
                            new User
                                {
                                    Id = 1,
                                    Name = "John Due",
                                    Email = "john.due@domain.com"
                                },
                            new User
                                {
                                    Id = 2,
                                    Name = "Jane Due",
                                    Email = "jane.due@domain.com"
                                }
                        };
        }

        public ActionResult Index()
        {
            return View(users);
        }

        public ViewResult Detail(int id)
        {
            return View("Edit", users.FirstOrDefault(user => user.Id == id));
        }

        [HttpPost]
        public ActionResult Update()
        {
            return RedirectToAction("Show");
        }

        public ActionResult Show()
        {
            return View("");
        }
    }
}
