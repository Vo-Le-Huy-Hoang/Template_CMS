using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using System.Transactions;

namespace WebApplication1.Controllers
{
    
    public class SAN_PHAMController : Controller
    {
        private CsK24T11Entities db = new CsK24T11Entities();

        // GET: SAN_PHAM
        public ActionResult Index()
        {
            var model = db.SAN_PHAM.ToList();
            return View(model);
        }
        [AllowAnonymous]
        public ActionResult Search(string keyword)
        {
            var model = db.SAN_PHAM.ToList();
            model = model.Where(p => p.TenSP.ToLower().Contains(keyword.ToLower())).ToList();
            ViewBag.Keyword = keyword;
            return View("Index2", model);
        }
        // for customer to view products
        [AllowAnonymous]
        public ActionResult Index2()
        {
            var model = db.SAN_PHAM.ToList();
            return View(model);
        }
        [AllowAnonymous]
        public ActionResult Details(int id)
        {
            var model = db.SAN_PHAM.Find(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }
        [AllowAnonymous]
        public ActionResult Picture(int id)
        {
            var path = Server.MapPath(PICTURE_PATH);
            return File(path + id, "images");
        }
        // GET: SAN_PHAM/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SAN_PHAM sAN_PHAM = db.SAN_PHAM.Find(id);
            if (sAN_PHAM == null)
            {
                return HttpNotFound();
            }
            return View(sAN_PHAM);
        }

        // GET: SAN_PHAM/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SAN_PHAM/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SAN_PHAM model, HttpPostedFileBase picture)
        {
            ValidateProduct(model);
            if (ModelState.IsValid)
            {
                if (picture != null)
                {
                    using (var scope = new TransactionScope())
                    {
                        db.SAN_PHAM.Add(model);
                        db.SaveChanges();

                        // store picture
                        var path = Server.MapPath(PICTURE_PATH);
                        picture.SaveAs(path + model.ID_SP);

                        scope.Complete();
                        return RedirectToAction("Index");
                    }
                }
                else ModelState.AddModelError("", "Picture not found!");
            }

            return View(model);
        }

        private const string PICTURE_PATH = "~/Upload/Products/";

        private void ValidateProduct(SAN_PHAM product)
        {
           /// if (product.Price < 0)
               /// ModelState.AddModelError("Price", "Price is less than Zero");
        }
        // GET: SAN_PHAM/Edit/5
        public ActionResult Edit(int id)
        {
            
            SAN_PHAM model = db.SAN_PHAM.Find(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_LoaiSP = new SelectList(db.LOAI_SAN_PHAM, "ID_LoaiSP", "TenLoaiSP", model.ID_LoaiSP);
            ViewBag.ID_NCC = new SelectList(db.NHA_CUNG_CAP, "ID_NCC", "TenNhaCC", model.ID_NCC);
            return View(model);
        }

        // POST: SAN_PHAM/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( SAN_PHAM sAN_PHAM)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sAN_PHAM).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_LoaiSP = new SelectList(db.LOAI_SAN_PHAM, "ID_LoaiSP", "TenLoaiSP", sAN_PHAM.ID_LoaiSP);
            ViewBag.ID_NCC = new SelectList(db.NHA_CUNG_CAP, "ID_NCC", "TenNhaCC", sAN_PHAM.ID_NCC);
            return View(sAN_PHAM);
        }

        // GET: SAN_PHAM/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SAN_PHAM sAN_PHAM = db.SAN_PHAM.Find(id);
            if (sAN_PHAM == null)
            {
                return HttpNotFound();
            }
            return View(sAN_PHAM);
        }

        // POST: SAN_PHAM/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
          
            SAN_PHAM sAN_PHAM = db.SAN_PHAM.Find(id);
            db.SAN_PHAM.Remove(sAN_PHAM);
            db.SaveChanges();
            return RedirectToAction("Index");
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
