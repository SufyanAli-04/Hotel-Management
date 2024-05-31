using System.Linq;
using System.Web.Mvc;
using Hotel_Management.Models;
using System.Data.Entity;
using HotelManagementSystem.Models;

namespace Hotel_Management.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            var bookings = db.Bookings.Include(b => b.Guest).Include(b => b.Room).ToList();
            return View(bookings);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }

        public ActionResult Search(string query)
        {
            if (string.IsNullOrEmpty(query))
            {
                return View("Index", db.Bookings.Include(b => b.Guest).Include(b => b.Room).ToList());
            }

            int id;
            bool isNumeric = int.TryParse(query, out id);

            if (isNumeric)
            {
                var results = db.Bookings
                    .Include(b => b.Guest)
                    .Include(b => b.Room)
                    .Where(b => b.Booking_ID == id ||
                                b.Guest.Guest_ID == id ||
                                b.Room.Room_ID == id)
                    .ToList();

                return View("Index", results);
            }
            else
            {
                var results = db.Bookings
                    .Include(b => b.Guest)
                    .Include(b => b.Room)
                    .Where(b => b.Guest.First_Name.Contains(query) ||
                                b.Guest.Last_Name.Contains(query) ||
                                b.Guest.Email.Contains(query) ||
                                b.Room.Room_Type.Contains(query))
                    .ToList();

                return View("Index", results);
            }
        }
    }
}
