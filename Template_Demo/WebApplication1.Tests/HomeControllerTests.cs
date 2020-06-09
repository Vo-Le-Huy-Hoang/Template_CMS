using System;
using System.Data;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApplication1.Controllers;

namespace WebApplication1.Tests
{
    [TestClass]
    public class HomeControllerTests
    {
        [TestMethod]
        public void Index()
        {
            var controller = new HomeController();

            var result = controller.Index() as ViewResult;

            Assert.AreEqual("Your application description page.", result.ViewData["Message"]);
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void Contact()
        {
            var controller = new HomeController();

            var result = controller.Contact() as ViewResult;

            Assert.AreEqual("Your contact page.", result.ViewData["Message"]);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void About()
        {
            var controller = new HomeController();

            var result = controller.About() as ViewResult;

            Assert.AreEqual("Your application description page.", result.ViewData["Message"]);
            Assert.IsNotNull(result);
        }
    }
}
