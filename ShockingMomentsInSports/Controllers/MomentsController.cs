using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ShockingMomentsInSports.Models;

namespace ShockingMomentsInSports.Controllers
{
    public class MomentsController : Controller
    {
        private ShockingSportsMomentsEntities db = new ShockingSportsMomentsEntities();

        // GET: Moments
        public ActionResult Index(string sport, string category, string searchQry)
        {
            //return View(db.Moments.ToList());

            // search by sport
            var SportList = new List<string>();
            var SportQry = from s in db.Moments
                           orderby s.Sport
                           select s.Sport;
            SportList.AddRange(SportQry.Distinct());
            ViewBag.sport = new SelectList(SportList);

            // search by league or category
            var CategoryList = new List<string>();
            var CategoryQry = from c in db.Moments
                              orderby c.LeagueOrCategory
                              select c.LeagueOrCategory;
            CategoryList.AddRange(CategoryQry.Distinct());
            ViewBag.category = new SelectList(CategoryList);

            // search by event

            // search by description

            // search by title
            var moments = from m in db.Moments
                          select m;

            if (!String.IsNullOrEmpty(searchQry))
            {
                moments = moments.Where(s => s.Title.Contains(searchQry));
            }

            //last bit of sport search
            if (!String.IsNullOrEmpty(sport))
            {
                moments = moments.Where(x => x.Sport == sport);
            }

            //last bit of league or category search
            if (!String.IsNullOrEmpty(category))
            {
                moments = moments.Where(x => x.LeagueOrCategory == category);
            }

            return View(moments);
        }
    

        // GET: Moments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Moment moment = db.Moments.Find(id);
            if (moment == null)
            {
                return HttpNotFound();
            }
            return View(moment);
        }

        // GET: Moments/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Moments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Sport,LeagueOrCategory,Event,Date,Description,YouTubeVideo")] Moment moment)
        {
            if (ModelState.IsValid)
            {
                db.Moments.Add(moment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(moment);
        }

        // GET: Moments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Moment moment = db.Moments.Find(id);
            if (moment == null)
            {
                return HttpNotFound();
            }
            return View(moment);
        }

        // POST: Moments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Sport,LeagueOrCategory,Event,Date,Description,YouTubeVideo")] Moment moment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(moment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(moment);
        }

        // GET: Moments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Moment moment = db.Moments.Find(id);
            if (moment == null)
            {
                return HttpNotFound();
            }
            return View(moment);
        }

        // POST: Moments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Moment moment = db.Moments.Find(id);
            db.Moments.Remove(moment);
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
