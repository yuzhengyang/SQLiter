using EFSqliteCodeFirst.DbTool;
using System.Linq;
using System.Web.Mvc;

namespace EFSqliteCodeFirst.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            //using (var db = new SqliteDbContext())
            //{
            //    return View(db.Set<Customer>().FirstOrDefault().Roles.FirstOrDefault().Customers.FirstOrDefault());
            //}
            using (var db = new Muse())
            {
                var a = db.Set<Book>().FirstOrDefault();//SQLite.CodeFirst版本0.9.5
                return View(a);
            }
        }
    }
}