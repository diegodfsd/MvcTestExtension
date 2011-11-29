using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Site.Controllers;
using Site.Models;

namespace Tests
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class UserControllerTests
    {
        [TestMethod]
        public void Index_ReturnsModelWithUserList()
        {
            UserController userController = new UserController();

            userController
                .Index()
                .ReturnsViewResult()
                .WithModel<IList<User>>()
                .ShouldNotBeNull();
        }

        [TestMethod]
        public void Detail_ReturnsViewEditWithModelUser()
        {
            UserController userController = new UserController();

            userController
                .Detail(1)
                .ReturnsViewResult()
                .WithName("Edit")
                .WithModel<User>()
                .ShouldBeEqual(1, u => u.Id)
                .ShouldBeEqual("John Due", u => u.Name);
        }

        [TestMethod]
        public void UpdatePost_ShouldBeAnnotatedWithHttpPost()
        {
            UserController userController = new UserController();

            userController
                .ActionHasAttribute<UserController>(u => u.Update(), typeof(HttpPostAttribute));
        }

        [TestMethod]
        public void UpdatePost_ReturnsRedirectToShowAction()
        {
            UserController userController = new UserController();

            userController
                .Update()
                .ReturnsRedirectToRouteResult()
                .ToAction<UserController>(u => u.Show());
        }
    }
}
