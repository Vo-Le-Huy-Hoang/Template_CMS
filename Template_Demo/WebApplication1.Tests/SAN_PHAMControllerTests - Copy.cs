﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApplication1.Controllers;
using System.Web.Mvc;
using System.Collections.Generic;
using WebApplication1.Models;
using System.Linq;

namespace WebApplication1.Tests
{
    [TestClass]
    public class SAN_PHAMControllerTests
    {
        [TestMethod]
        public void TestIndex()
        {
            var controller = new SAN_PHAMController();

            var result = controller.Index() as ViewResult;
            Assert.IsNotNull(result);

            var Model = result.Model as List<SAN_PHAM>;
            Assert.IsNotNull(result);

            var db = new CsK24T11Entities();
            Assert.AreEqual(db.SAN_PHAM.Count(), Model.Count);
        }
        [TestMethod]
        public void TestIndex2()
        {
            var controller = new SAN_PHAMController();

            var result = controller.Index2() as ViewResult;
            Assert.IsNotNull(result);

            var model = result.Model as List<SAN_PHAM>;
            Assert.IsNotNull(model);
        }
    }
}
