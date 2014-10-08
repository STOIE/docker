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
    public class ConsumerRepAreaOfInterestController : Controller
    {
        private HCCADatabaseContext db = new HCCADatabaseContext();

        // GET: /ConsumerRepAreaOfInterest/
        public async Task<ActionResult> Index()
        {
            return View(await db.ConsumerRepAreasOfInterest.ToListAsync());
        }

        // GET: /ConsumerRepAreaOfInterest/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ConsumerRepAreaOfInterestModel consumerrepareaofinterestmodel = await db.ConsumerRepAreasOfInterest.FindAsync(id);
            if (consumerrepareaofinterestmodel == null)
            {
                return HttpNotFound();
            }
            return View(consumerrepareaofinterestmodel);
        }

       

        // GET: /ConsumerRepAreaOfInterest/Create
        public ActionResult Create()
        {
            
            return View();
        }

        // POST: /ConsumerRepAreaOfInterest/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="AreaOfInterestName")] ConsumerRepAreaOfInterestModel consumerrepareaofinterestmodel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.ConsumerRepAreasOfInterest.Add(consumerrepareaofinterestmodel);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException /* dex */)
            {
                // Log the error
                ModelState.AddModelError("", "Unable to save changes. Please try again.");
            }

            return View(consumerrepareaofinterestmodel);
        }

        // GET: /ConsumerRepAreaOfInterest/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ConsumerRepAreaOfInterestModel consumerrepareaofinterestmodel = await db.ConsumerRepAreasOfInterest.FindAsync(id);
            if (consumerrepareaofinterestmodel == null)
            {
                return HttpNotFound();
            }
            return View(consumerrepareaofinterestmodel);
        }

        // POST: /ConsumerRepAreaOfInterest/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="ConsumerRepAreaOfInterestModelID,AreaOfInterestName")] ConsumerRepAreaOfInterestModel consumerrepareaofinterestmodel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(consumerrepareaofinterestmodel).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            catch(DataException /* dex */)
            {
                // Log the error 
                ModelState.AddModelError("", "Unable to save changes. Please try again.");
            }
            return View(consumerrepareaofinterestmodel);
        }

        // GET: /ConsumerRepAreaOfInterest/Delete/5
        public async Task<ActionResult> Delete(int? id, bool? saveChangesError=false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if(saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete failed. Please try again";
            }
            ConsumerRepAreaOfInterestModel consumerrepareaofinterestmodel = await db.ConsumerRepAreasOfInterest.FindAsync(id);
            if (consumerrepareaofinterestmodel == null)
            {
                return HttpNotFound();
            }
            return View(consumerrepareaofinterestmodel);
        }

        // POST: /ConsumerRepAreaOfInterest/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                ConsumerRepAreaOfInterestModel consumerrepareaofinterestmodel = await db.ConsumerRepAreasOfInterest.FindAsync(id);
                db.ConsumerRepAreasOfInterest.Remove(consumerrepareaofinterestmodel);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch(DataException /* dex */)
            {
                // Log the error
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
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
