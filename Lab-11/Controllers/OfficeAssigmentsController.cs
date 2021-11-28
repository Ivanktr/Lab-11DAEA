using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Lab_11;
using Lab_11.Models;

namespace Lab_11.Controllers
{
    public class OfficeAssigmentsController : Controller
    {
        private SchoolContext db = new SchoolContext();

        // GET: OfficeAssigments
        public ActionResult Index()
        {
            var officeAssigments = db.OfficeAssigments.Include(o => o.Instructor);
            return View(officeAssigments.Where(o=>o.Activo == true).ToList());
        }

        // GET: OfficeAssigments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OfficeAssigment officeAssigment = db.OfficeAssigments.Find(id);
            if (officeAssigment == null)
            {
                return HttpNotFound();
            }
            return View(officeAssigment);
        }

        // GET: OfficeAssigments/Create
        public ActionResult Create()
        {
            ViewBag.InstructorID = new SelectList(db.People, "ID", "LastName");
            return View();
        }

        // POST: OfficeAssigments/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "InstructorID,location,Activo")] OfficeAssigment officeAssigment)
        {
            if (ModelState.IsValid)
            {
                officeAssigment.Activo = true;
                db.OfficeAssigments.Add(officeAssigment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.InstructorID = new SelectList(db.People, "ID", "LastName", officeAssigment.InstructorID);
            return View(officeAssigment);
        }

        // GET: OfficeAssigments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OfficeAssigment officeAssigment = db.OfficeAssigments.Find(id);
            if (officeAssigment == null)
            {
                return HttpNotFound();
            }
            ViewBag.InstructorID = new SelectList(db.People, "ID", "LastName", officeAssigment.InstructorID);
            return View(officeAssigment);
        }

        // POST: OfficeAssigments/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "InstructorID,location,Activo")] OfficeAssigment officeAssigment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(officeAssigment).State = EntityState.Modified;
                officeAssigment.Activo = true;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.InstructorID = new SelectList(db.People, "ID", "LastName", officeAssigment.InstructorID);
            return View(officeAssigment);
        }

        // GET: OfficeAssigments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OfficeAssigment officeAssigment = db.OfficeAssigments.Find(id);
            if (officeAssigment == null)
            {
                return HttpNotFound();
            }
            return View(officeAssigment);
        }

        // POST: OfficeAssigments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OfficeAssigment officeAssigment = db.OfficeAssigments.Find(id);
            db.Entry(officeAssigment).State = EntityState.Modified;
            officeAssigment.Activo = false;
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
