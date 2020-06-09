using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class CHI_TIET_HOA_DONController : Controller
    {
        private CsK24T11Entities db = new CsK24T11Entities();
        private List<CHI_TIET_HOA_DON> ShoppingCart = null;

        public CHI_TIET_HOA_DONController()
        {
            var session = System.Web.HttpContext.Current.Session;
            if (session["ShoppingCart"] != null)
                ShoppingCart = session["ShoppingCart"] as List<CHI_TIET_HOA_DON>;
            else
            {
                ShoppingCart = new List<CHI_TIET_HOA_DON>();
                session["ShoppingCart"] = ShoppingCart;
            }
        }

        // GET: CHI_TIET_HOA_DON
        public ActionResult Index()
        {

            return View(ShoppingCart);
        }

        
        // GET: CHI_TIET_HOA_DON/Create
        [HttpPost]
        public ActionResult Create(int ID_ChiTietHD, int SL )
        {
            var product = db.SAN_PHAM.Find(SL);
            ShoppingCart.Add(new CHI_TIET_HOA_DON
            {
                SAN_PHAM = product,
                SL = SL

            });
            return RedirectToAction("Index");
        }

        
        // GET: CHI_TIET_HOA_DON/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CHI_TIET_HOA_DON cHI_TIET_HOA_DON = db.CHI_TIET_HOA_DON.Find(id);
            if (cHI_TIET_HOA_DON == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_HoaDon = new SelectList(db.HOA_DON, "ID_HoaDon", "NoiGiaoHang", cHI_TIET_HOA_DON.ID_HoaDon);
            ViewBag.ID_SP = new SelectList(db.SAN_PHAM, "ID_SP", "TenSP", cHI_TIET_HOA_DON.ID_SP);
            return View(cHI_TIET_HOA_DON);
        }

        

        // GET: CHI_TIET_HOA_DON/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CHI_TIET_HOA_DON cHI_TIET_HOA_DON = db.CHI_TIET_HOA_DON.Find(id);
            if (cHI_TIET_HOA_DON == null)
            {
                return HttpNotFound();
            }
            return View(cHI_TIET_HOA_DON);
        }

        

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
