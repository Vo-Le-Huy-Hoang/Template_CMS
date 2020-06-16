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
    public class SAN_PHAMController : Controller
    {
        private CsK24T11Entities db = new CsK24T11Entities();

        // GET: SAN_PHAM
        public ActionResult Index()
        {
            var sAN_PHAM = db.SAN_PHAM.Include(s => s.LOAI_SAN_PHAM).Include(s => s.NHA_CUNG_CAP);
            return View(sAN_PHAM.ToList());
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
            ViewBag.ID_LoaiSP = new SelectList(db.LOAI_SAN_PHAM, "ID_LoaiSP", "TenLoaiSP");
            ViewBag.ID_NCC = new SelectList(db.NHA_CUNG_CAP, "ID_NCC", "TenNhaCC");
            return View();
        }

        // POST: SAN_PHAM/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_SP,ID_NCC,ID_LoaiSP,TenSP,MoTa,IMAGE")] SAN_PHAM sAN_PHAM)
        {
            if (ModelState.IsValid)
            {
                db.SAN_PHAM.Add(sAN_PHAM);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_LoaiSP = new SelectList(db.LOAI_SAN_PHAM, "ID_LoaiSP", "TenLoaiSP", sAN_PHAM.ID_LoaiSP);
            ViewBag.ID_NCC = new SelectList(db.NHA_CUNG_CAP, "ID_NCC", "TenNhaCC", sAN_PHAM.ID_NCC);
            return View(sAN_PHAM);
        }

        // GET: SAN_PHAM/Edit/5
        public ActionResult Edit(int? id)
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
            ViewBag.ID_LoaiSP = new SelectList(db.LOAI_SAN_PHAM, "ID_LoaiSP", "TenLoaiSP", sAN_PHAM.ID_LoaiSP);
            ViewBag.ID_NCC = new SelectList(db.NHA_CUNG_CAP, "ID_NCC", "TenNhaCC", sAN_PHAM.ID_NCC);
            return View(sAN_PHAM);
        }

        // POST: SAN_PHAM/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_SP,ID_NCC,ID_LoaiSP,TenSP,MoTa,IMAGE")] SAN_PHAM sAN_PHAM)
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
