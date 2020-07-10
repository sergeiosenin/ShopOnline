using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using ShopOnline.Models;

namespace ShopOnline.Controllers
{
    public class OrdersController : Controller
    {
        private OrderContext db = new OrderContext();

        // GET: Orders
        public ActionResult Index()
        {
            var orders = db.Orders.Include(o => o.Client).Include(o => o.Product).ToList();
            return View(orders);
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            db.Entry(order).Reference(x => x.Client).Load();
            db.Entry(order).Reference(x => x.Product).Load();
            if (order.Client != null)
            {
                var client = db.Clients.FirstOrDefault(x => x.ID == order.Client.ID);
                if (client != null)
                    ViewBag.Client = $"{client.Name} {client.LastName}";
            }
            if (order.Product != null)
            {
                var product = db.Products.FirstOrDefault(x => x.ID == order.Product.ID);
                if (product != null)
                    ViewBag.Product = product.Name;
            }
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            ViewBag.Client = new SelectList(db.Clients.Select(x => new { ID = x.ID, FullName = x.Name + " " + x.LastName }), "ID", "FullName");
            ViewBag.Product = new SelectList(db.Products, "ID", "Name");
            return View();
        }

        // POST: Orders/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ProductCount,Date")] Order order)
        {
            if (!ModelState.IsValid)
            {
                if (ModelState["Client"].Errors.Any())
                    ViewBag.Client = new SelectList(db.Clients.Select(x => new { ID = x.ID, FullName = x.Name + " " + x.LastName }), "ID", "FullName");
                if (ModelState["Product"].Errors.Any())
                    ViewBag.Product = new SelectList(db.Products, "ID", "Name");
                var client = db.Clients.ToList().FirstOrDefault(x => x.ID == Convert.ToInt32(Request["Client"]));
                var product = db.Products.ToList().FirstOrDefault(x => x.ID == Convert.ToInt32(Request["Product"]));
                order.Client = client;
                order.Product = product;
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            if (ModelState.IsValid)
            {
                var client = db.Clients.ToList().FirstOrDefault(x => x.ID == Convert.ToInt32(Request["Client"]));
                var product = db.Products.ToList().FirstOrDefault(x => x.ID == Convert.ToInt32(Request["Product"]));
                order.Client = client;
                order.Product = product;
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(order);
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            db.Entry(order).Reference(x => x.Client).Load();
            db.Entry(order).Reference(x => x.Product).Load();
            ViewBag.Client = new SelectList(db.Clients.Select(x => new { ID = x.ID, FullName = x.Name + " " + x.LastName }), "ID", "FullName", order.Client?.ID);
            ViewBag.Product = new SelectList(db.Products, "ID", "Name", order.Product?.ID);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ProductCount,Date")] Order order)
        {
            
            ViewBag.Client = new SelectList(db.Clients.Select(x => new { ID = x.ID, FullName = x.Name + " " + x.LastName }), "ID", "FullName", order.Client?.ID);
            ViewBag.Product = new SelectList(db.Products, "ID", "Name", order.Product?.ID);

            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                var client = db.Clients.ToList().FirstOrDefault(x => x.ID == Convert.ToInt32(Request["Client"]));
                var product = db.Products.ToList().FirstOrDefault(x => x.ID == Convert.ToInt32(Request["Product"]));
                order.Client = client;
                order.Product = product;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(order);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
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
