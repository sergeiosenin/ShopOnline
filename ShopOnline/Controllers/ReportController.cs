using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using ShopOnline.Models;

namespace ShopOnline.Controllers
{
    public class ReportController : Controller
    {
        private OrderContext db = new OrderContext();

        // GET: Report
        //public ActionResult Index()
        //{
        //    var orders = db.Orders.Include(o => o.Client).Include(o => o.Product).ToList();
        //    return View(orders);
        //}
        public ActionResult Index(DateTime? beginDate, DateTime? endDate)
        {
            if (beginDate == null) beginDate = DateTime.MinValue;
            if (endDate == null) endDate = DateTime.Now;

            var orders = db.Orders.Include(o => o.Client).Include(o => o.Product).Where(x => x.Date >= beginDate && x.Date <= endDate).ToList();
            ViewBag.OrderCount = orders.Count;
            ViewBag.OrderSum = orders.Sum(x => x.Product?.Cost * x.ProductCount);
            return View(orders);
        }

    }
}