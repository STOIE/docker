using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HCCADBWebAppPrototype1.Models;
using HCCADBWebAppPrototype1.DAL;

namespace HCCADBWebAppPrototype1.Controllers
{
    public class ConsumerRepModel_ConsumerRepAreaOfInterestController : Controller
    {
        private HCCADatabaseContext db = new HCCADatabaseContext();

        // GET: /ConsumerRepModel_ConsumerRepAreaOfInterest/
        public async Task<ActionResult> Index()
        {
            var consumerrepmodel_consumerrepareasofinterestmodel = db.ConsumerRepModel_ConsumerRepAreasOfInterestModel.Include(c => c.ConsumerRepModel);
            return View(await consumerrepmodel_consumerrepareasofinterestmodel.ToListAsync());
        }

        // GET: /ConsumerRepModel_ConsumerRepAreaOfInterest/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ConsumerRepModel_ConsumerRepAreaOfInterestModel consumerrepmodel_consumerrepareaofinterestmodel = await db.ConsumerRepModel_ConsumerRepAreasOfInterestModel.FindAsync(id);
            if (consumerrepmodel_consumerrepareaofinterestmodel == null)
            {
                return HttpNotFound();
            }
            return View(consumerrepmodel_consumerrepareaofinterestmodel);
        }
        // GET: /ConsumerRepModel_ConsumerRepAreaOfInterest/AddConsumerToInterest
        public ActionResult AddConsumerToInterest(int? id)
        {
            // Add code to catch null
            ViewBag.ConsumerRepModelID = id;
            ViewBag.ConsumerRepAreaOfInterestModelID = new SelectList(db.ConsumerRepAreasOfInterest, "ConsumerRepAreaOfInterestModelID", "AreaOfInterestName");
            return View();
        }

        // POST: /ConsumerRepModel_ConsumerRepAreaOfInterest/AddConsumerToInterest
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddConsumerToInterest([Bind(Include="ConsumerRepModel_ConsumerRepAreaOfInterestModelID,ConsumerRepModelID,ConsumerRepAreaOfInterestModelID")] ConsumerRepModel_ConsumerRepAreaOfInterestModel consumerrepmodel_consumerrepareaofinterestmodel)
        {
            if (ModelState.IsValid)
            {
                db.ConsumerRepModel_ConsumerRepAreasOfInterestModel.Add(consumerrepmodel_consumerrepareaofinterestmodel);
                await db.SaveChangesAsync();
                return RedirectToAction("Details/" + consumerrepmodel_consumerrepareaofinterestmodel.ConsumerRepModelID, "ConsumerRepModel");
            }

            ViewBag.ConsumerRepModelID = consumerrepmodel_consumerrepareaofinterestmodel.ConsumerRepModelID;
            ViewBag.ConsumerRepAreaOfInterestModelID = new SelectList(db.ConsumerRepAreasOfInterest, "ConsumerRepAreaOfInterestModelID", "AreaOfInterestName");
            return View(consumerrepmodel_consumerrepareaofinterestmodel);
        }

        // GET: /ConsumerRepModel_ConsumerRepAreaOfInterest/Create
        public ActionResult Create()
        {
            ViewBag.ConsumerRepModelID = new SelectList(db.ConsumerReps, "ConsumerRepModelID", "FirstName");
            ViewBag.ConsumerRepAreaOfInterestModelID = new SelectList(db.ConsumerRepAreasOfInterest, "ConsumerRepAreaOfInterestModelID", "AreaOfInterestName");
            return View();
        }

        // POST: /ConsumerRepModel_ConsumerRepAreaOfInterest/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="ConsumerRepModel_ConsumerRepAreaOfInterestModelID,ConsumerRepModelID,ConsumerRepAreaOfInterestModelID")] ConsumerRepModel_ConsumerRepAreaOfInterestModel consumerrepmodel_consumerrepareaofinterestmodel)
        {
            if (ModelState.IsValid)
            {
                db.ConsumerRepModel_ConsumerRepAreasOfInterestModel.Add(consumerrepmodel_consumerrepareaofinterestmodel);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ConsumerRepModelID = new SelectList(db.ConsumerReps, "ConsumerRepModelID", "FirstName", consumerrepmodel_consumerrepareaofinterestmodel.ConsumerRepModelID);
            ViewBag.ConsumerRepAreaOfInterestModelID = new SelectList(db.ConsumerRepAreasOfInterest, "ConsumerRepAreaOfInterestModelID", "AreaOfInterestName");
            return View(consumerrepmodel_consumerrepareaofinterestmodel);
        }

        // GET: /ConsumerRepModel_ConsumerRepAreaOfInterest/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ConsumerRepModel_ConsumerRepAreaOfInterestModel consumerrepmodel_consumerrepareaofinterestmodel = await db.ConsumerRepModel_ConsumerRepAreasOfInterestModel.FindAsync(id);
            if (consumerrepmodel_consumerrepareaofinterestmodel == null)
            {
                return HttpNotFound();
            }
            ViewBag.ConsumerRepModelID = new SelectList(db.ConsumerReps, "ConsumerRepModelID", "FirstName", consumerrepmodel_consumerrepareaofinterestmodel.ConsumerRepModelID);
            return View(consumerrepmodel_consumerrepareaofinterestmodel);
        }

        // POST: /ConsumerRepModel_ConsumerRepAreaOfInterest/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="ConsumerRepModel_ConsumerRepAreaOfInterestModelID,ConsumerRepModelID,ConsumerRepAreaOfInterestModelID")] ConsumerRepModel_ConsumerRepAreaOfInterestModel consumerrepmodel_consumerrepareaofinterestmodel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(consumerrepmodel_consumerrepareaofinterestmodel).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ConsumerRepModelID = new SelectList(db.ConsumerReps, "ConsumerRepModelID", "FirstName", consumerrepmodel_consumerrepareaofinterestmodel.ConsumerRepModelID);
            return View(consumerrepmodel_consumerrepareaofinterestmodel);
        }

        // GET: /ConsumerRepModel_ConsumerRepAreaOfInterest/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ConsumerRepModel_ConsumerRepAreaOfInterestModel consumerrepmodel_consumerrepareaofinterestmodel = await db.ConsumerRepModel_ConsumerRepAreasOfInterestModel.FindAsync(id);
            if (consumerrepmodel_consumerrepareaofinterestmodel == null)
            {
                return HttpNotFound();
            }
            return View(consumerrepmodel_consumerrepareaofinterestmodel);
        }

        // POST: /ConsumerRepModel_ConsumerRepAreaOfInterest/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ConsumerRepModel_ConsumerRepAreaOfInterestModel consumerrepmodel_consumerrepareaofinterestmodel = await db.ConsumerRepModel_ConsumerRepAreasOfInterestModel.FindAsync(id);
            db.ConsumerRepModel_ConsumerRepAreasOfInterestModel.Remove(consumerrepmodel_consumerrepareaofinterestmodel);
            await db.SaveChangesAsync();
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
