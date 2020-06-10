using System;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApplication1.Controllers;

namespace Template_Demo_Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var controller = new HomeController();
            var result = controller.Index() as ViewResult;
            Assert.IsNotNull(result);

            result = controller.About() as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("Your application description page.", result.ViewBag.Message);
        }
    }
}
