using M6_C8_P1.Models;
using M6_C8_P1.Repositories.Interfaces;
using M6_C8_P1.Repositories;
using M6_C8_P1.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;
using X.PagedList;
using X.PagedList.Mvc;
using X.PagedList.Mvc.Common;

namespace M6_C8_P1.Controllers
{

    [Authorize]
    public class TeamsController : Controller
    {
        private readonly CricketDbContext db = new CricketDbContext();

        IGenericRepo<Team> repo;
        public TeamsController()
        {
            this.repo = new GenericRepo<Team>(db);
        }
        // GET: Applicants
        [AllowAnonymous]
        public ActionResult Index(int pg = 1)
        {

            var data = this.repo.GetAll("Players").ToPagedList(pg, 5);
            return View(data);

        }

        public ActionResult Create()
        {
            TeamInputModel a = new TeamInputModel();
            a.Players.Add(new Player { });
            return View(a);
        }
        [HttpPost]
        public ActionResult Create(TeamInputModel data, string act = "")
        {
            if (act == "add")
            {
                data.Players.Add(new Player { });

                foreach (var item in ModelState.Values)
                {
                    item.Errors.Clear();
                }
            }
            if (act.StartsWith("remove"))
            {
                int index = int.Parse(act.Substring(act.IndexOf("_") + 1));
                data.Players.RemoveAt(index);

                foreach (var item in ModelState.Values)
                {
                    item.Errors.Clear();
                }
            }

            if (act == "insert")
            {

                if (ModelState.IsValid)
                {

                    var a = new Team
                    {
                        TeamName = data.TeamName,
                        Established = data.Established,
                        Coach = data.Coach,
                        Country = data.Country,
                        Captain = data.Captain,
                        BoardPresident = data.BoardPresident,
                        HomeGround = data.HomeGround,
                        IsWCWinner = data.IsWCWinner,
                        Ranking = data.Ranking
                    };

                    string ext = Path.GetExtension(data.TeamLogo.FileName);
                    string fileName = Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + ext;
                    string savePath = Server.MapPath("~/Pictures/") + fileName;
                    data.TeamLogo.SaveAs(savePath);
                    a.TeamLogo = fileName;

                    foreach (var q in data.Players)
                    {

                        a.Players.Add(q);
                    }
                    this.repo.Insert(a);
                }
            }

            ViewBag.Act = act;

            return PartialView("_CreatePartial",data);
        }

        public ActionResult Edit(int id)
        {
            var x = this.repo.Get(id, "players"); ;
           
              var a = new TeamEditModel
              {
                  Id = x.Id,
                  TeamName = x.TeamName,
                  Established = x.Established,
                  Coach = x.Coach,
                  Country = x.Country,
                  Captain = x.Captain,
                  BoardPresident = x.BoardPresident,
                  HomeGround = x.HomeGround,
                  IsWCWinner = x.IsWCWinner,
                  Ranking = x.Ranking,
                  Players = x.Players.ToList()
              };

            ViewBag.CurrentPic = db.Teams.First(t => t.Id == id).TeamLogo;

            return View(a);
        }

        [HttpPost]
        public ActionResult Edit(TeamEditModel data, string act = "")
        {

            if (act == "add")
            {

                data.Players.Add(new Player { });
            }

            if (act.StartsWith("remove"))
            {

                int index = int.Parse(act.Substring(act.IndexOf("_") + 1));
                data.Players.RemoveAt(index);
            }

            if (act == "update")
            {

                if (ModelState.IsValid)
                {

                    var a = this.repo.Get(data.Id);

                    a.TeamName = data.TeamName;
                    a.Established = data.Established;
                    a.Coach = data.Coach;
                    a.Country = data.Country;
                    a.Captain = data.Captain;
                    a.BoardPresident = data.BoardPresident;
                    a.HomeGround = data.HomeGround;
                    a.IsWCWinner = data.IsWCWinner;
                    a.Ranking = data.Ranking;

                    if (data.TeamLogo != null)
                    {

                        string ext = Path.GetExtension(data.TeamLogo.FileName);
                        string fileName = Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + ext;
                        string savePath = Server.MapPath("~/Pictures/") + fileName;
                        data.TeamLogo.SaveAs(savePath);
                        a.TeamLogo = fileName;
                    }

                    this.repo.Update(a);
                    this.repo.ExecuteCommand($"DELETE FROM Players WHERE TeamId={a.Id}");

                    foreach (var item in data.Players)
                    {
                        this.repo.ExecuteCommand($"INSERT INTO Players ([PlayerName], [Age], [PlayingRole], [Matches], [Runs], [Hundred], [Fifty], [Wicket], [Salary], TeamId) VALUES ('{item.PlayerName}', '{item.Age}', '{item.PlayingRole}', '{item.Matches}', '{item.Runs}','{item.Hundred}', '{item.Fifty}','{item.Wicket}','{item.Salary}', {a.Id})");
                    }

                    return RedirectToAction("Index");
                }
            }
            ViewBag.CurrentPic = db.Teams.First(x => x.Id == data.Id).TeamLogo;

            return View(data);
        }        
        [HttpPost]
        public ActionResult Delete(int id)
        {
            this.repo.ExecuteCommand($"dbo.DeleteTeam {id}");
            return Json(new { success = true, deleted = id });
        }

    }
}
