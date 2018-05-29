using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AsociatieProprietari.Models;

namespace AsociatieProprietari.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ContractController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Contrat
        public ActionResult Index()
        {
            return View(db.ContratModels.ToList());
        }

        // GET: Contrat/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContratModels contratModels = db.ContratModels.Find(id);
            if (contratModels == null)
            {
                return HttpNotFound();
            }
            return View(contratModels);
        }

        // GET: Contrat/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Contrat/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Supplier,Resource,Value")] ContratModels contratModels)
        {
            if (ModelState.IsValid)
            {
                db.ContratModels.Add(contratModels);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(contratModels);
        }

        // GET: Contrat/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContratModels contratModels = db.ContratModels.Find(id);
            if (contratModels == null)
            {
                return HttpNotFound();
            }
            return View(contratModels);
        }

        // POST: Contrat/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Supplier,Resource,Value")] ContratModels contratModels)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contratModels).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(contratModels);
        }

        // GET: Contrat/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContratModels contratModels = db.ContratModels.Find(id);
            if (contratModels == null)
            {
                return HttpNotFound();
            }
            return View(contratModels);
        }

        // POST: Contrat/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ContratModels contratModels = db.ContratModels.Find(id);
            db.ContratModels.Remove(contratModels);
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
