using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Hotel_Management.Models;

namespace Hotel_Management.Controllers
{
    public class SearchesController : Controller
    {
        private ClassEntities db = new ClassEntities();

        // GET: Searches
        public ActionResult Index()
        {
            var searches = db.Searches.Include(s => s.Booking).Include(s => s.Guest).Include(s => s.Room);
            return View(searches.ToList());
        }

        // GET: Searches/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Search search = db.Searches.Find(id);
            if (search == null)
            {
                return HttpNotFound();
            }
            return View(search);
        }

        // GET: Searches/Create
        public ActionResult Create()
        {
            ViewBag.Booking_ID = new SelectList(db.Bookings, "Booking_ID", "Booking_ID");
            ViewBag.Guest_ID = new SelectList(db.Guests, "Guest_ID", "First_Name");
            ViewBag.Rooms_ID = new SelectList(db.Rooms, "Room_ID", "Room_Type");
            return View();
        }

        // POST: Searches/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "T_Person,Booking_ID,Guest_ID,Rooms_ID")] Search search)
        {
            if (ModelState.IsValid)
            {
                db.Searches.Add(search);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Booking_ID = new SelectList(db.Bookings, "Booking_ID", "Booking_ID", search.Booking_ID);
            ViewBag.Guest_ID = new SelectList(db.Guests, "Guest_ID", "First_Name", search.Guest_ID);
            ViewBag.Rooms_ID = new SelectList(db.Rooms, "Room_ID", "Room_Type", search.Rooms_ID);
            return View(search);
        }

        // GET: Searches/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Search search = db.Searches.Find(id);
            if (search == null)
            {
                return HttpNotFound();
            }
            ViewBag.Booking_ID = new SelectList(db.Bookings, "Booking_ID", "Booking_ID", search.Booking_ID);
            ViewBag.Guest_ID = new SelectList(db.Guests, "Guest_ID", "First_Name", search.Guest_ID);
            ViewBag.Rooms_ID = new SelectList(db.Rooms, "Room_ID", "Room_Type", search.Rooms_ID);
            return View(search);
        }

        // POST: Searches/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "T_Person,Booking_ID,Guest_ID,Rooms_ID")] Search search)
        {
            if (ModelState.IsValid)
            {
                db.Entry(search).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Booking_ID = new SelectList(db.Bookings, "Booking_ID", "Booking_ID", search.Booking_ID);
            ViewBag.Guest_ID = new SelectList(db.Guests, "Guest_ID", "First_Name", search.Guest_ID);
            ViewBag.Rooms_ID = new SelectList(db.Rooms, "Room_ID", "Room_Type", search.Rooms_ID);
            return View(search);
        }

        // GET: Searches/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Search search = db.Searches.Find(id);
            if (search == null)
            {
                return HttpNotFound();
            }
            return View(search);
        }

        // POST: Searches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Search search = db.Searches.Find(id);
            db.Searches.Remove(search);
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
