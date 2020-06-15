using Moq;
using System;
using System.Web;
using System.Linq;
using System.Web.Mvc;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApplication1.Controllers;
using WebApplication1.Models;
using System.Transactions;

namespace WebApplication.Tests.Controllers
{
    [TestClass]
    public class ProductsControllerTest
    {
        [TestMethod]
        public void TestIndex()
        {
            var controller = new SAN_PHAMController();

            var result = controller.Index() as ViewResult;
            Assert.IsNotNull(result);

            var model = result.Model as List<SAN_PHAM>;
            Assert.IsNotNull(model);

            var db = new CsK24T11Entities();
            Assert.AreEqual(db.SAN_PHAM.Count(), model.Count);
        }

        [TestMethod]
        public void TestIndex2()
        {
            var controller = new SAN_PHAMController();

            var result = controller.Index2() as ViewResult;
            Assert.IsNotNull(result);

            var model = result.Model as List<SAN_PHAM>;
            Assert.IsNotNull(model);

            var db = new CsK24T11Entities();
            Assert.AreEqual(db.SAN_PHAM.Count(), model.Count);
        }

        [TestMethod]
        public void TestCreateG()
        {
            var controller = new SAN_PHAMController();

            var result = controller.Create() as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestCreateP()
        {
            var rand = new Random();
            var product = new SAN_PHAM
            {
                TenSP = rand.NextDouble().ToString(),
                MoTa = rand.NextDouble().ToString(),
                Price = -rand.Next()
            };

            var controller = new SAN_PHAMController();

            var result0 = controller.Create(product, null) as ViewResult;
            Assert.IsNotNull(result0);
            Assert.AreEqual("Price is less than Zero", controller.ModelState["Price"].Errors[0].ErrorMessage);

            product.Price = -product.Price;
            controller.ModelState.Clear();

            result0 = controller.Create(product, null) as ViewResult;
            Assert.IsNotNull(result0);
            Assert.AreEqual("Picture not found!", controller.ModelState[""].Errors[0].ErrorMessage);

            var picture = new Mock<HttpPostedFileBase>();
            var server = new Mock<HttpServerUtilityBase>();
            var context = new Mock<HttpContextBase>();
            context.Setup(c => c.Server).Returns(server.Object);
            controller.ControllerContext = new ControllerContext(context.Object,
                new System.Web.Routing.RouteData(), controller);

            var fileName = String.Empty;
            server.Setup(s => s.MapPath(It.IsAny<string>())).Returns<string>(s => s);
            picture.Setup(p => p.SaveAs(It.IsAny<string>())).Callback<string>(s => fileName = s);

            using (var scope = new TransactionScope())
            {
                controller.ModelState.Clear();
                var result1 = controller.Create(product, picture.Object) as RedirectToRouteResult;
                Assert.IsNotNull(result1);
                Assert.AreEqual("Index", result1.RouteValues["action"]);

                var db = new CsK24T11Entities();
                var entity = db.SAN_PHAM.SingleOrDefault(p => p.TenSP == product.TenSP && p.MoTa == product.MoTa);
                Assert.IsNotNull(entity);
                Assert.AreEqual(product.Price, entity.Price);

                Assert.IsTrue(fileName.StartsWith("~/Upload/Products/"));
                Assert.IsTrue(fileName.EndsWith(entity.ID_SP.ToString()));
            }
        }

        [TestMethod]
        public void TestEditG()
        {
            var controller = new SAN_PHAMController();
            var result0 = controller.Edit(0) as HttpNotFoundResult;
            Assert.IsNotNull(result0);

            var db = new CsK24T11Entities();
            var product = db.SAN_PHAM.First();
            var result1 = controller.Edit(product.ID_SP) as ViewResult;
            Assert.IsNotNull(result1);

            var model = result1.Model as SAN_PHAM;
            Assert.IsNotNull(model);
            Assert.AreEqual(product.TenSP, model.TenSP);
            Assert.AreEqual(product.Price, model.Price);
            Assert.AreEqual(product.MoTa, model.MoTa);
        }

        [TestMethod]
        public void TestEditP()
        {
            var rand = new Random();
            var db = new CsK24T11Entities();
            var product = db.SAN_PHAM.AsNoTracking().First();
            product.TenSP = rand.NextDouble().ToString();
            product.MoTa = rand.NextDouble().ToString();
            product.Price = -rand.Next();

            var controller = new SAN_PHAMController();

            var result0 = controller.Edit(product, null) as ViewResult;
            Assert.IsNotNull(result0);
            Assert.AreEqual("Price is less than Zero", controller.ModelState["Price"].Errors[0].ErrorMessage);

            var picture = new Mock<HttpPostedFileBase>();
            var server = new Mock<HttpServerUtilityBase>();
            var context = new Mock<HttpContextBase>();
            context.Setup(c => c.Server).Returns(server.Object);
            controller.ControllerContext = new ControllerContext(context.Object,
                new System.Web.Routing.RouteData(), controller);

            var fileName = String.Empty;
            server.Setup(s => s.MapPath(It.IsAny<string>())).Returns<string>(s => s);
            picture.Setup(p => p.SaveAs(It.IsAny<string>())).Callback<string>(s => fileName = s);

            using (var scope = new TransactionScope())
            {
                product.Price = -product.Price;
                controller.ModelState.Clear();
                var result1 = controller.Edit(product, picture.Object) as RedirectToRouteResult;
                Assert.IsNotNull(result1);
                Assert.AreEqual("Index", result1.RouteValues["action"]);

                var entity = db.SAN_PHAM.Find(product.ID_SP);
                Assert.IsNotNull(entity);
                Assert.AreEqual(product.TenSP, entity.TenSP);
                Assert.AreEqual(product.MoTa, entity.MoTa);
                Assert.AreEqual(product.Price, entity.Price);

                Assert.AreEqual("~/Upload/Products/" + product.ID_SP, fileName);
                //Assert.IsTrue(fileName.StartsWith("~/Upload/Products/"));
                //Assert.IsTrue(fileName.EndsWith(entity.id.ToString()));
            }
        }

        [TestMethod]
        public void TestDeleteG()
        {
            var controller = new SAN_PHAMController();
            var result0 = controller.Delete(0) as HttpNotFoundResult;
            Assert.IsNotNull(result0);

            var db = new CsK24T11Entities();
            var product = db.SAN_PHAM.First();
            var result1 = controller.Delete(product.ID_SP) as ViewResult;
            Assert.IsNotNull(result1);

            var model = result1.Model as SAN_PHAM;
            Assert.IsNotNull(model);
            Assert.AreEqual(product.TenSP, model.TenSP);
            Assert.AreEqual(product.Price, model.Price);
            Assert.AreEqual(product.MoTa, model.MoTa);
        }

        [TestMethod]
        public void TestDeleteP()
        {
            var db = new CsK24T11Entities();
            var product = db.SAN_PHAM.AsNoTracking().First();

            var controller = new SAN_PHAMController();

            var context = new Mock<HttpContextBase>();
            var server = new Mock<HttpServerUtilityBase>();
            context.Setup(c => c.Server).Returns(server.Object);
            controller.ControllerContext = new ControllerContext(context.Object,
                new System.Web.Routing.RouteData(), controller);

            var filePath = String.Empty;
            var tempDir = System.IO.Path.GetTempFileName();
            server.Setup(s => s.MapPath(It.IsAny<string>())).Returns<string>(s =>
            {
                filePath = s;
                return tempDir;
            });

            using (var scope = new TransactionScope())
            {
                System.IO.File.Delete(tempDir);
                System.IO.Directory.CreateDirectory(tempDir);
                tempDir = tempDir + "/";
                System.IO.File.Create(tempDir + product.ID_SP).Close();
                Assert.IsTrue(System.IO.File.Exists(tempDir + product.ID_SP));

                var result = controller.DeleteConfirmed(product.ID_SP) as RedirectToRouteResult;
                Assert.IsNotNull(result);
                Assert.AreEqual("Index", result.RouteValues["action"]);

                var entity = db.SAN_PHAM.Find(product.ID_SP);
                Assert.IsNull(entity);

                Assert.AreEqual("~/Upload/Products/", filePath);
                Assert.IsFalse(System.IO.File.Exists(tempDir + product.ID_SP));
            }
        }

        [TestMethod]
        public void TestPicture()
        {
            var controller = new SAN_PHAMController();

            var context = new Mock<HttpContextBase>();
            var server = new Mock<HttpServerUtilityBase>();
            context.Setup(c => c.Server).Returns(server.Object);
            controller.ControllerContext = new ControllerContext(context.Object,
                new System.Web.Routing.RouteData(), controller);

            var filePath = String.Empty;
            server.Setup(s => s.MapPath(It.IsAny<string>())).Returns<string>(s => filePath = s);

            var result = controller.Picture(0) as FilePathResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("~/Upload/Products/0", result.FileName);
            Assert.AreEqual("images", result.ContentType);
        }

        [TestMethod]
        public void TestDispose()
        {
            using (var controller = new SAN_PHAMController()) { }
        }

        [TestMethod]
        public void TestDetails()
        {
            var controller = new SAN_PHAMController();
            var result0 = controller.Details(0) as HttpNotFoundResult;
            Assert.IsNotNull(result0);

            var db = new CsK24T11Entities();
            var product = db.SAN_PHAM.First();
            var result1 = controller.Details(product.ID_SP) as ViewResult;
            Assert.IsNotNull(result1);

            var model = result1.Model as SAN_PHAM;
            Assert.IsNotNull(model);
            Assert.AreEqual(product.TenSP, model.TenSP);
            Assert.AreEqual(product.Price, model.Price);
            Assert.AreEqual(product.MoTa, model.MoTa);
        }

        [TestMethod]
        public void TestSearch()
        {
            var db = new CsK24T11Entities();
            var products = db.SAN_PHAM.ToList();
            var keyword = products.First().TenSP.Split().First();
            products = products.Where(p => p.TenSP.ToLower().Contains(keyword.ToLower())).ToList();

            var controller = new SAN_PHAMController();
            var result = controller.Search(keyword) as ViewResult;
            Assert.IsNotNull(result);

            var model = result.Model as List<SAN_PHAM>;
            Assert.IsNotNull(model);

            Assert.AreEqual("Index2", result.ViewName);
            Assert.AreEqual(products.Count(), model.Count);
            Assert.AreEqual(keyword, result.ViewData["Keyword"]);
        }
    }
}
