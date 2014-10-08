using HCCADBWebAppPrototype1.BusinessLogic;
using HCCADBWebAppPrototype1.DAL;
using HCCADBWebAppPrototype1.Models;
using HCCADBWebAppPrototype1.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace HCCADBWebAppPrototype1.Controllers
{
    public class CommitteeModelController : Controller
    {
        private HCCADatabaseContext db = new HCCADatabaseContext();

        private Util_CommitteeModelController utils_Committees = new Util_CommitteeModelController();

        // GET: /CommitteeModel/
        public ActionResult Index(CommitteeIndexViewModel viewModel, string searchByStatus, string searchByArea)
        {

            int pageSize = 20;
            int pageNumber = (viewModel.page ?? 1);

            if (String.IsNullOrEmpty(searchByStatus))
            {
                searchByStatus = "Current";
            }

            List<string> CommitteeStatuses = new List<string>();
            CommitteeStatuses.Add("Current");
            CommitteeStatuses.Add("Past");
            CommitteeStatuses.Add("All");
            ViewBag.CommitteeStatuses = new SelectList(CommitteeStatuses);

            List<string> AreasOfHealth = new List<string>();
            AreasOfHealth.Add("All");
            foreach (var interest in db.CommitteeAreaOfHealth)
            {
                AreasOfHealth.Add(interest.AreaOfHealthName);
            }
            ViewBag.AreasOfHealth = new SelectList(AreasOfHealth);

            var committees = from com in db.Committees
                             select com;

            if (!String.IsNullOrEmpty(searchByArea) && searchByArea != "All")
            {
                committees = from com in committees
                             from joinComInterest in db.CommitteeModel_CommitteeAreaOfHealth
                             where com.CommitteeModelID == joinComInterest.CommitteeModelID
                             where joinComInterest.CommitteeAreaOfHealthModel.AreaOfHealthName == searchByArea
                             select com;
            }

            if (searchByStatus == "Current")
            {
                CurrentStatus status = CurrentStatus.Current;

                committees = from com in committees 
                             where com.CurrentStatus == status
                             select com;

                committees = committees.OrderBy(cr => cr.CommitteeName);
                viewModel.CommitteeModels = committees.ToPagedList(pageNumber, pageSize);
                return View(viewModel);
            }
            else if (searchByStatus == "Past")
            {
                CurrentStatus status = CurrentStatus.Past;

                committees = from com in committees
                             where com.CurrentStatus == status
                             select com;

                committees = committees.OrderBy(cr => cr.CommitteeName);
                viewModel.CommitteeModels = committees.ToPagedList(pageNumber, pageSize);
                return View(viewModel); ;

            }


            committees = committees.OrderBy(cr => cr.CommitteeName);
            viewModel.CommitteeModels = committees.ToPagedList(pageNumber, pageSize);
            return View(viewModel);

            #region Old code
            /*
            if (String.IsNullOrEmpty(searchByStatus))
            {
                searchByStatus = "Current"; 
            }

            List<string> CommitteeStatuses = new List<string>();
            CommitteeStatuses.Add("Current");
            CommitteeStatuses.Add("Past");
            CommitteeStatuses.Add("All");
            ViewBag.CommitteeStatuses = new SelectList(CommitteeStatuses);

            DateTime currentDate = DateTime.Today;

            DateTime Start = new DateTime(currentDate.Year, 1, 1);
            DateTime MiddleStart = new DateTime(currentDate.Year, 6, 30);
            DateTime MiddleEnd = new DateTime(currentDate.Year, 7, 1);
            DateTime End = new DateTime(currentDate.Year, 12, 31);

            if (searchByStatus == "Current")
            {
                if (currentDate >= Start && currentDate <= MiddleStart)
                { 
                    var currentCommittees = from com in db.Committees
                        from joinComHist in db.ConsumerRepCommitteeHistory
                        where com.CommitteeModelID == joinComHist.CommitteeModelID
                        where joinComHist.EndorsementDate < MiddleEnd && (joinComHist.EndorsementDate > Start || joinComHist.EndorsementDate == Start)
                        select com;

                    return View(await currentCommittees.ToListAsync());
                }
                else if (currentDate >= MiddleEnd && currentDate <= End)
                { 
                    var currentCommittees = from com in db.Committees
                            from joinComHist in db.ConsumerRepCommitteeHistory
                            where com.CommitteeModelID == joinComHist.CommitteeModelID
                            where joinComHist.EndorsementDate > MiddleStart && (joinComHist.EndorsementDate < End || joinComHist.EndorsementDate == End)
                            select com;

                    return View(await currentCommittees.ToListAsync());
                }
            }
            else if (searchByStatus == "Past")
            { 
                if (currentDate >= Start && currentDate <= MiddleStart)
                { 
                    var currentCommittees = from com in db.Committees
                        from joinComHist in db.ConsumerRepCommitteeHistory
                        where com.CommitteeModelID == joinComHist.CommitteeModelID
                        where joinComHist.EndorsementDate < Start 
                        select com;

                    return View(await currentCommittees.ToListAsync());
                }
                else if (currentDate >= MiddleEnd && currentDate <= End)
                { 
                    var currentCommittees = from com in db.Committees
                            from joinComHist in db.ConsumerRepCommitteeHistory
                            where com.CommitteeModelID == joinComHist.CommitteeModelID
                            where joinComHist.EndorsementDate < MiddleStart 
                            select com;

                    return View(await currentCommittees.ToListAsync());
                }

            }
            return View(await db.Committees.ToListAsync());
            */
            #endregion
        }

        // GET: /CommitteeModel/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CommitteeModel committeemodel = await db.Committees.FindAsync(id);
            if (committeemodel == null)
            {
                return HttpNotFound();
            }
            return View(committeemodel);
        }

        // GET: /CommitteeModel/Create
        public ActionResult Create()
        {
            var vm = new CreateCommitteeViewModel
            {
                NewCommitteeModel = new CommitteeModel(),
                NewConsumerRepCommitteeHistoryModel = new ConsumerRepCommitteeHistoryModel(),
                NewCommitteeAreaOfHealthModel = new CommitteeModel_CommitteeAreaOfHealthModel(),
                ConsumerRepsID = new SelectList(db.ConsumerReps, "ConsumerRepModelID", "FullName"),
                CommitteeAreasOfHealthID = new SelectList(db.CommitteeAreaOfHealth, "CommitteeAreaOfHealthModelID", "AreaOfHealthName")
            };

            return View(vm);
        }

        // POST: /CommitteeModel/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateCommitteeViewModel vm)
        {
            try
            {
                //if (ModelState.IsValid)
                //{
                    vm.NewCommitteeModel.CurrentStatus = CurrentStatus.Current;
                    db.Committees.Add(vm.NewCommitteeModel);

                    vm.NewConsumerRepCommitteeHistoryModel.ReportedDate = DateTime.Today;

                    vm.NewConsumerRepCommitteeHistoryModel.CommitteeModelID = vm.NewCommitteeModel.CommitteeModelID;
                    db.ConsumerRepCommitteeHistory.Add(vm.NewConsumerRepCommitteeHistoryModel);

                    vm.NewCommitteeAreaOfHealthModel.CommitteeModelID = vm.NewCommitteeModel.CommitteeModelID;
                    db.CommitteeModel_CommitteeAreaOfHealth.Add(vm.NewCommitteeAreaOfHealthModel);

                    ConsumerRepModel consumer = db.ConsumerReps.Find(vm.NewConsumerRepCommitteeHistoryModel.ConsumerRepModelID);
                    consumer.EndorsementStatus = EndorsementStatus.Active;
                    db.Entry(consumer).State = EntityState.Modified;

                    db.SaveChanges();

                    return RedirectToAction("Index");
                //}
            }
            catch(DataException /* dex */)
            {
                // Log the error
                ModelState.AddModelError("", "Unable to save the chances. Please try again.");
            }

            vm = new CreateCommitteeViewModel
            {
                NewCommitteeModel = new CommitteeModel(),
                NewConsumerRepCommitteeHistoryModel = new ConsumerRepCommitteeHistoryModel(),
                NewCommitteeAreaOfHealthModel = new CommitteeModel_CommitteeAreaOfHealthModel(),
                ConsumerRepsID = new SelectList(db.ConsumerReps, "ConsumerRepModelID", "FullName"),
                CommitteeAreasOfHealthID = new SelectList(db.CommitteeAreaOfHealth, "CommitteeAreaOfHealthModelID", "AreaOfHealthName")
            };

            return View(vm);
        }
        // GET: /CommitteeModel/Create
        public ActionResult CreateStandalone()
        {
            return View();
        }

        // POST: /CommitteeModel/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateStandalone([Bind(Include="CommitteeName,CurrentStatus")] CommitteeModel committee)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    db.Committees.Add(committee);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
            }
            catch (DataException /* dex */)
            {
                // Log the error
                ModelState.AddModelError("", "Unable to save the chances. Please try again.");
            }

            return View(committee);
        }
        // GET: /CommitteeModel/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CommitteeModel committeemodel = await db.Committees.FindAsync(id);
            if (committeemodel == null)
            {
                return HttpNotFound();
            }
            return View(committeemodel);
        }

        // POST: /CommitteeModel/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="CommitteeModelID,CommitteeName,CurrentStatus")] CommitteeModel committeemodel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(committeemodel).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            catch(DataException /* dex */)
            {
                // Log the error
                ModelState.AddModelError("", "Unable to save chanes. Please try again.");
            }

            return View(committeemodel);
        }

        // GET: /CommitteeModel/Edit/5
        public async Task<ActionResult> SetInActive(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Couldnt set to InActive. Please try again";
            }
            CommitteeModel committeemodel = await db.Committees.FindAsync(id);
            if (committeemodel == null)
            {
                return HttpNotFound();
            }
            return View(committeemodel);
        }

        // POST: /CommitteeModel/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SetInActive(int id)
        {
            try
            {
                CommitteeModel committeemodel = await db.Committees.FindAsync(id);
                committeemodel.CurrentStatus = CurrentStatus.Past;
                db.Entry(committeemodel).State = EntityState.Modified;

                var committeehistory = from comHistory in db.ConsumerRepCommitteeHistory
                                       where comHistory.CommitteeModelID == committeemodel.CommitteeModelID 
                                       select comHistory;


                foreach (var comHistory in committeehistory)
                { 
                    comHistory.FinishedDate = DateTime.Today;
                    db.Entry(comHistory).State = EntityState.Modified;

                    ConsumerRepModel consumerRep = db.ConsumerReps.Find(comHistory.ConsumerRepModelID);
                    consumerRep.EndorsementStatus = EndorsementStatus.InActive;
                    db.Entry(consumerRep).State = EntityState.Modified;
                }

                await db.SaveChangesAsync();

            }
            catch (DataException /* dex */)
            {
                return RedirectToAction("SetInActive", new { id = id, saveChancesError = true });
            }
            return RedirectToAction("Index");
        }


        // GET: /CommitteeModel/Delete/5
        public async Task<ActionResult> Delete(int? id, bool? saveChangesError=false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if(saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete Failed. Please try again";
            }
            CommitteeModel committeemodel = await db.Committees.FindAsync(id);
            if (committeemodel == null)
            {
                return HttpNotFound();
            }
            return View(committeemodel);
        }

        // POST: /CommitteeModel/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                CommitteeModel committeemodel = await db.Committees.FindAsync(id);
                db.Committees.Remove(committeemodel);
                await db.SaveChangesAsync();
            }
            catch(DataException /* dex */)
            {
                return RedirectToAction("Delete", new { id = id, saveChancesError = true });
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
