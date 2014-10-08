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
    public class CommitteeAreaOfHealthModelController : Controller
    {
        private HCCADatabaseContext db = new HCCADatabaseContext();

        // GET: /CommitteeAreaOfHealthModel/
        public async Task<ActionResult> Index()
        {
            return View(await db.CommitteeAreaOfHealth.ToListAsync());
        }

        // GET: /CommitteeAreaOfHealthModel/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CommitteeAreaOfHealthModel committeeareaofhealthmodel = await db.CommitteeAreaOfHealth.FindAsync(id);
            if (committeeareaofhealthmodel == null)
            {
                return HttpNotFound();
            }
            return View(committeeareaofhealthmodel);
        }

        // GET: /CommitteeAreaOfHealthModel/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /CommitteeAreaOfHealthModel/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="CommitteeAreaOfHealthModelID,AreaOfHealthName")] CommitteeAreaOfHealthModel committeeareaofhealthmodel)
        {
            if (ModelState.IsValid)
            {
                db.CommitteeAreaOfHealth.Add(committeeareaofhealthmodel);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(committeeareaofhealthmodel);
        }

        // GET: /CommitteeAreaOfHealthModel/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CommitteeAreaOfHealthModel committeeareaofhealthmodel = await db.CommitteeAreaOfHealth.FindAsync(id);
            if (committeeareaofhealthmodel == null)
            {
                return HttpNotFound();
            }
            return View(committeeareaofhealthmodel);
        }

        // POST: /CommitteeAreaOfHealthModel/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="CommitteeAreaOfHealthModelID,AreaOfHealthName")] CommitteeAreaOfHealthModel committeeareaofhealthmodel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(committeeareaofhealthmodel).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(committeeareaofhealthmodel);
        }

        // GET: /CommitteeAreaOfHealthModel/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CommitteeAreaOfHealthModel committeeareaofhealthmodel = await db.CommitteeAreaOfHealth.FindAsync(id);
            if (committeeareaofhealthmodel == null)
            {
                return HttpNotFound();
            }
            return View(committeeareaofhealthmodel);
        }

        // POST: /CommitteeAreaOfHealthModel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            CommitteeAreaOfHealthModel committeeareaofhealthmodel = await db.CommitteeAreaOfHealth.FindAsync(id);
            db.CommitteeAreaOfHealth.Remove(committeeareaofhealthmodel);
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
