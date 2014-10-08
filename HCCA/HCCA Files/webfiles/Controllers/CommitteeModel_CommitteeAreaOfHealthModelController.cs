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
    public class CommitteeModel_CommitteeAreaOfHealthModelController : Controller
    {
        private HCCADatabaseContext db = new HCCADatabaseContext();

        // GET: /CommitteeModel_CommitteeAreaOfHealthModel/
        public async Task<ActionResult> Index()
        {
            var committeemodel_committeeareaofhealth = db.CommitteeModel_CommitteeAreaOfHealth.Include(c => c.CommitteeAreaOfHealthModel).Include(c => c.CommitteeModel);
            return View(await committeemodel_committeeareaofhealth.ToListAsync());
        }

        // GET: /CommitteeModel_CommitteeAreaOfHealthModel/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CommitteeModel_CommitteeAreaOfHealthModel committeemodel_committeeareaofhealthmodel = await db.CommitteeModel_CommitteeAreaOfHealth.FindAsync(id);
            if (committeemodel_committeeareaofhealthmodel == null)
            {
                return HttpNotFound();
            }
            return View(committeemodel_committeeareaofhealthmodel);
        }

        // GET: /CommitteeModel_CommitteeAreaOfHealthModel/Create
        public ActionResult AddCommitteeToArea(int? id)
        {
            // Add code to catch null
            ViewBag.CommitteeAreaOfHealthModelID = new SelectList(db.CommitteeAreaOfHealth, "CommitteeAreaOfHealthModelID", "AreaOfHealthName");
            ViewBag.CommitteeModelID = id;
            return View();
        }

        // POST: /CommitteeModel_CommitteeAreaOfHealthModel/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddCommitteeToArea([Bind(Include="CommitteeModel_CommitteeAreaOfHealthModelID,CommitteeModelID,CommitteeAreaOfHealthModelID")] CommitteeModel_CommitteeAreaOfHealthModel committeemodel_committeeareaofhealthmodel)
        {
            if (ModelState.IsValid)
            {
                db.CommitteeModel_CommitteeAreaOfHealth.Add(committeemodel_committeeareaofhealthmodel);
                await db.SaveChangesAsync();
                return RedirectToAction("Details/" + committeemodel_committeeareaofhealthmodel.CommitteeModelID, "CommitteeModel");
            }

            ViewBag.CommitteeAreaOfHealthModelID = new SelectList(db.CommitteeAreaOfHealth, "CommitteeAreaOfHealthModelID", "AreaOfHealthName", committeemodel_committeeareaofhealthmodel.CommitteeAreaOfHealthModelID);
            ViewBag.CommitteeModelID = committeemodel_committeeareaofhealthmodel.CommitteeModelID;
            return View(committeemodel_committeeareaofhealthmodel);
        }

        // GET: /CommitteeModel_CommitteeAreaOfHealthModel/Create
        public ActionResult Create()
        {
            ViewBag.CommitteeAreaOfHealthModelID = new SelectList(db.CommitteeAreaOfHealth, "CommitteeAreaOfHealthModelID", "AreaOfHealthName");
            ViewBag.CommitteeModelID = new SelectList(db.Committees, "CommitteeModelID", "CommitteeName");
            return View();
        }

        // POST: /CommitteeModel_CommitteeAreaOfHealthModel/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="CommitteeModel_CommitteeAreaOfHealthModelID,CommitteeModelID,CommitteeAreaOfHealthModelID")] CommitteeModel_CommitteeAreaOfHealthModel committeemodel_committeeareaofhealthmodel)
        {
            if (ModelState.IsValid)
            {
                db.CommitteeModel_CommitteeAreaOfHealth.Add(committeemodel_committeeareaofhealthmodel);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CommitteeAreaOfHealthModelID = new SelectList(db.CommitteeAreaOfHealth, "CommitteeAreaOfHealthModelID", "AreaOfHealthName", committeemodel_committeeareaofhealthmodel.CommitteeAreaOfHealthModelID);
            ViewBag.CommitteeModelID = new SelectList(db.Committees, "CommitteeModelID", "CommitteeName", committeemodel_committeeareaofhealthmodel.CommitteeModelID);
            return View(committeemodel_committeeareaofhealthmodel);
        }

        // GET: /CommitteeModel_CommitteeAreaOfHealthModel/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CommitteeModel_CommitteeAreaOfHealthModel committeemodel_committeeareaofhealthmodel = await db.CommitteeModel_CommitteeAreaOfHealth.FindAsync(id);
            if (committeemodel_committeeareaofhealthmodel == null)
            {
                return HttpNotFound();
            }
            ViewBag.CommitteeAreaOfHealthModelID = new SelectList(db.CommitteeAreaOfHealth, "CommitteeAreaOfHealthModelID", "AreaOfHealthName", committeemodel_committeeareaofhealthmodel.CommitteeAreaOfHealthModelID);
            ViewBag.CommitteeModelID = new SelectList(db.Committees, "CommitteeModelID", "CommitteeName", committeemodel_committeeareaofhealthmodel.CommitteeModelID);
            return View(committeemodel_committeeareaofhealthmodel);
        }

        // POST: /CommitteeModel_CommitteeAreaOfHealthModel/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="CommitteeModel_CommitteeAreaOfHealthModelID,CommitteeModelID,CommitteeAreaOfHealthModelID")] CommitteeModel_CommitteeAreaOfHealthModel committeemodel_committeeareaofhealthmodel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(committeemodel_committeeareaofhealthmodel).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CommitteeAreaOfHealthModelID = new SelectList(db.CommitteeAreaOfHealth, "CommitteeAreaOfHealthModelID", "AreaOfHealthName", committeemodel_committeeareaofhealthmodel.CommitteeAreaOfHealthModelID);
            ViewBag.CommitteeModelID = new SelectList(db.Committees, "CommitteeModelID", "CommitteeName", committeemodel_committeeareaofhealthmodel.CommitteeModelID);
            return View(committeemodel_committeeareaofhealthmodel);
        }

        // GET: /CommitteeModel_CommitteeAreaOfHealthModel/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CommitteeModel_CommitteeAreaOfHealthModel committeemodel_committeeareaofhealthmodel = await db.CommitteeModel_CommitteeAreaOfHealth.FindAsync(id);
            if (committeemodel_committeeareaofhealthmodel == null)
            {
                return HttpNotFound();
            }
            return View(committeemodel_committeeareaofhealthmodel);
        }

        // POST: /CommitteeModel_CommitteeAreaOfHealthModel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            CommitteeModel_CommitteeAreaOfHealthModel committeemodel_committeeareaofhealthmodel = await db.CommitteeModel_CommitteeAreaOfHealth.FindAsync(id);
            db.CommitteeModel_CommitteeAreaOfHealth.Remove(committeemodel_committeeareaofhealthmodel);
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
