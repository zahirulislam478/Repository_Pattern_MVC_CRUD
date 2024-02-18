using M6_C8_P1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using X.PagedList;

namespace M6_C8_P1.Controllers
{
    public class PlayersController : Controller
    {
        // GET: Players
        private readonly CricketDbContext db = new CricketDbContext();

        [AllowAnonymous]
        public ActionResult Index(int pg = 1)
        {
            var data = db.Players.OrderBy(a => a.Id).ToPagedList(pg, 11);
            return View(data);

        }
    }
}