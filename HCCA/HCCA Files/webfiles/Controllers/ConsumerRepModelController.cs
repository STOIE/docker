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
    public class ConsumerRepModelController : Controller
    {
        private Util_ConsumerRepModelController utils_ConRep = new Util_ConsumerRepModelController();
        private HCCADatabaseContext db = new HCCADatabaseContext();

        // GET: /ConsumerRepModel/
        public ActionResult Index(ConsumerRepIndexViewModel viewModel, string sortOrder, string searchByInterest, string searchByStatus, string searchByEndorseStatus, string searchByName, string startDate, string endDate, string search, string reset)
        {
            if (!String.IsNullOrEmpty(search))
            {
                try
                {
                    int pageSize = 20;
                    int pageNumber = (viewModel.page ?? 1);

                    if (String.IsNullOrEmpty(searchByStatus))
                    {
                        searchByStatus = "Yes";
                    }

                    if (String.IsNullOrEmpty(searchByEndorseStatus))
                    {
                        searchByEndorseStatus = "Yes";
                    }

                    List<string> EndorseStatuses = new List<string>();
                    EndorseStatuses.Add("Yes");
                    EndorseStatuses.Add("No");
                    EndorseStatuses.Add("All");

                    ViewBag.EndorseStatuses = new SelectList(EndorseStatuses);

                    List<string> MemberStatusTypes = new List<string>();
                    MemberStatusTypes.Add("Yes");
                    MemberStatusTypes.Add("No");
                    MemberStatusTypes.Add("All");

                    ViewBag.MemberStatusTypes = new SelectList(MemberStatusTypes);

                    List<string> AreasOfInterest = new List<string>();
                    AreasOfInterest.Add("All");
                    foreach (var interest in db.ConsumerRepAreasOfInterest)
                    {
                        AreasOfInterest.Add(interest.AreaOfInterestName);
                    }
                    ViewBag.AreasOfInterest = new SelectList(AreasOfInterest);

                    ViewBag.NameSortParam = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
                    //ViewBag.MemberStatusSortParam = sortOrder == "MemberStatus" ? "memberstatus_desc" : "MemberStatus";

                    var consumerReps = from cr in db.ConsumerReps
                                       select cr;

                    var consumerRepsByName = from cr in db.ConsumerReps
                                             where cr.FirstName.ToUpper().Contains(searchByName.ToUpper()) || cr.LastName.ToUpper().Contains(searchByName.ToUpper())
                                             select cr;

                    var consumerRepsByInterest = from cr in db.ConsumerReps
                                                 from joinCrInte in db.ConsumerRepModel_ConsumerRepAreasOfInterestModel
                                                 where cr.ConsumerRepModelID == joinCrInte.ConsumerRepModelID
                                                 where joinCrInte.ConsumerRepAreaOfInterestModel.AreaOfInterestName == searchByInterest
                                                 select cr;

                    var consumerRepsByInterestByName = from cr in db.ConsumerReps
                                                       from joinCrInte in db.ConsumerRepModel_ConsumerRepAreasOfInterestModel
                                                       where cr.ConsumerRepModelID == joinCrInte.ConsumerRepModelID
                                                       where joinCrInte.ConsumerRepAreaOfInterestModel.AreaOfInterestName == searchByInterest
                                                       where cr.FirstName.ToUpper().Contains(searchByName.ToUpper()) || cr.LastName.ToUpper().Contains(searchByName.ToUpper())
                                                       select cr;

                    ///
                    /// Consumer Reps By DateTrained
                    ///
                    var consumerRepsByDateTrained = from cr in db.ConsumerReps
                                                    select cr;

                    if (!String.IsNullOrEmpty(startDate) && !String.IsNullOrEmpty(endDate))
                    {
                        DateTime StartDate = Convert.ToDateTime(startDate);
                        DateTime EndDate = Convert.ToDateTime(endDate);

                        consumerRepsByDateTrained = from cr in db.ConsumerReps
                                                    where cr.DateTrained >= StartDate && cr.DateTrained <= EndDate
                                                    select cr;
                    }
                    else if (!String.IsNullOrEmpty(startDate) && String.IsNullOrEmpty(endDate))
                    {
                        DateTime StartDate = Convert.ToDateTime(startDate);

                        consumerRepsByDateTrained = from cr in db.ConsumerReps
                                                    where cr.DateTrained >= StartDate
                                                    select cr;
                    }
                    else if (String.IsNullOrEmpty(startDate) && !String.IsNullOrEmpty(endDate))
                    {
                        DateTime EndDate = Convert.ToDateTime(endDate);

                        consumerRepsByDateTrained = from cr in db.ConsumerReps
                                                    where cr.DateTrained <= EndDate
                                                    select cr;
                    }

                    ///
                    /// Consumer Reps By DateTrained, Name 
                    ///
                    var consumerRepsByDateTrainedByName = from cr in db.ConsumerReps
                                                          select cr;

                    if (!String.IsNullOrEmpty(startDate) && !String.IsNullOrEmpty(endDate))
                    {
                        DateTime StartDate = Convert.ToDateTime(startDate);
                        DateTime EndDate = Convert.ToDateTime(endDate);

                        consumerRepsByDateTrainedByName = from cr in db.ConsumerReps
                                                          where cr.DateTrained >= StartDate && cr.DateTrained <= EndDate
                                                          where cr.FirstName.ToUpper().Contains(searchByName.ToUpper()) || cr.LastName.ToUpper().Contains(searchByName.ToUpper())
                                                          select cr;
                    }
                    else if (!String.IsNullOrEmpty(startDate) && String.IsNullOrEmpty(endDate))
                    {
                        DateTime StartDate = Convert.ToDateTime(startDate);

                        consumerRepsByDateTrainedByName = from cr in db.ConsumerReps
                                                          where cr.DateTrained >= StartDate
                                                          where cr.FirstName.ToUpper().Contains(searchByName.ToUpper()) || cr.LastName.ToUpper().Contains(searchByName.ToUpper())
                                                          select cr;
                    }
                    else if (String.IsNullOrEmpty(startDate) && !String.IsNullOrEmpty(endDate))
                    {
                        DateTime EndDate = Convert.ToDateTime(endDate);

                        consumerRepsByDateTrainedByName = from cr in db.ConsumerReps
                                                          where cr.DateTrained <= EndDate
                                                          where cr.FirstName.ToUpper().Contains(searchByName.ToUpper()) || cr.LastName.ToUpper().Contains(searchByName.ToUpper())
                                                          select cr;
                    }

                    ///
                    /// Consumer Reps By DateTrained, Interest 
                    ///
                    var consumerRepsByDateTrainedByInterest = from cr in db.ConsumerReps
                                                              select cr;

                    if (!String.IsNullOrEmpty(startDate) && !String.IsNullOrEmpty(endDate))
                    {
                        DateTime StartDate = Convert.ToDateTime(startDate);
                        DateTime EndDate = Convert.ToDateTime(endDate);

                        consumerRepsByDateTrainedByInterest = from cr in db.ConsumerReps
                                                              from joinCrInte in db.ConsumerRepModel_ConsumerRepAreasOfInterestModel
                                                              where cr.ConsumerRepModelID == joinCrInte.ConsumerRepModelID
                                                              where joinCrInte.ConsumerRepAreaOfInterestModel.AreaOfInterestName == searchByInterest
                                                              where cr.DateTrained >= StartDate && cr.DateTrained <= EndDate
                                                              select cr;
                    }
                    else if (!String.IsNullOrEmpty(startDate) && String.IsNullOrEmpty(endDate))
                    {
                        DateTime StartDate = Convert.ToDateTime(startDate);

                        consumerRepsByDateTrainedByInterest = from cr in db.ConsumerReps
                                                              from joinCrInte in db.ConsumerRepModel_ConsumerRepAreasOfInterestModel
                                                              where cr.ConsumerRepModelID == joinCrInte.ConsumerRepModelID
                                                              where joinCrInte.ConsumerRepAreaOfInterestModel.AreaOfInterestName == searchByInterest
                                                              where cr.DateTrained >= StartDate
                                                              select cr;
                    }
                    else if (String.IsNullOrEmpty(startDate) && !String.IsNullOrEmpty(endDate))
                    {
                        DateTime EndDate = Convert.ToDateTime(endDate);

                        consumerRepsByDateTrainedByInterest = from cr in db.ConsumerReps
                                                              from joinCrInte in db.ConsumerRepModel_ConsumerRepAreasOfInterestModel
                                                              where cr.ConsumerRepModelID == joinCrInte.ConsumerRepModelID
                                                              where joinCrInte.ConsumerRepAreaOfInterestModel.AreaOfInterestName == searchByInterest
                                                              where cr.DateTrained <= EndDate
                                                              select cr;
                    }

                    ///
                    /// Consumer Reps By DateTrained, Interest & Name
                    ///
                    var consumerRepsByDateTrainedByInterestByName = from cr in db.ConsumerReps
                                                                    select cr;

                    if (!String.IsNullOrEmpty(startDate) && !String.IsNullOrEmpty(endDate))
                    {
                        DateTime StartDate = Convert.ToDateTime(startDate);
                        DateTime EndDate = Convert.ToDateTime(endDate);

                        consumerRepsByDateTrainedByInterestByName = from cr in db.ConsumerReps
                                                                    from joinCrInte in db.ConsumerRepModel_ConsumerRepAreasOfInterestModel
                                                                    where cr.ConsumerRepModelID == joinCrInte.ConsumerRepModelID
                                                                    where joinCrInte.ConsumerRepAreaOfInterestModel.AreaOfInterestName == searchByInterest
                                                                    where cr.FirstName.ToUpper().Contains(searchByName.ToUpper()) || cr.LastName.ToUpper().Contains(searchByName.ToUpper())
                                                                    where cr.DateTrained >= StartDate && cr.DateTrained <= EndDate
                                                                    select cr;
                    }
                    else if (!String.IsNullOrEmpty(startDate) && String.IsNullOrEmpty(endDate))
                    {
                        DateTime StartDate = Convert.ToDateTime(startDate);

                        consumerRepsByDateTrainedByInterestByName = from cr in db.ConsumerReps
                                                                    from joinCrInte in db.ConsumerRepModel_ConsumerRepAreasOfInterestModel
                                                                    where cr.ConsumerRepModelID == joinCrInte.ConsumerRepModelID
                                                                    where joinCrInte.ConsumerRepAreaOfInterestModel.AreaOfInterestName == searchByInterest
                                                                    where cr.FirstName.ToUpper().Contains(searchByName.ToUpper()) || cr.LastName.ToUpper().Contains(searchByName.ToUpper())
                                                                    where cr.DateTrained >= StartDate
                                                                    select cr;
                    }
                    else if (String.IsNullOrEmpty(startDate) && !String.IsNullOrEmpty(endDate))
                    {
                        DateTime EndDate = Convert.ToDateTime(endDate);

                        consumerRepsByDateTrainedByInterestByName = from cr in db.ConsumerReps
                                                                    from joinCrInte in db.ConsumerRepModel_ConsumerRepAreasOfInterestModel
                                                                    where cr.ConsumerRepModelID == joinCrInte.ConsumerRepModelID
                                                                    where joinCrInte.ConsumerRepAreaOfInterestModel.AreaOfInterestName == searchByInterest
                                                                    where cr.FirstName.ToUpper().Contains(searchByName.ToUpper()) || cr.LastName.ToUpper().Contains(searchByName.ToUpper())
                                                                    where cr.DateTrained <= EndDate
                                                                    select cr;
                    }

                    #region Search Logic
                    if ((!String.IsNullOrEmpty(startDate) || !String.IsNullOrEmpty(endDate)) && !String.IsNullOrEmpty(searchByStatus) && !String.IsNullOrEmpty(searchByName) && (!String.IsNullOrEmpty(searchByInterest) && searchByInterest != "All"))
                    {
                        MemberStatus _searchByStatus = MemberStatus.Yes;

                        if (searchByStatus == "Yes")
                        {
                            _searchByStatus = MemberStatus.Yes;
                            consumerRepsByDateTrainedByInterestByName = consumerRepsByDateTrainedByInterestByName.Where(cr => cr.MemberStatus.Value == _searchByStatus);
                        }
                        else if (searchByStatus == "No")
                        {
                            _searchByStatus = MemberStatus.No;
                            consumerRepsByDateTrainedByInterestByName = consumerRepsByDateTrainedByInterestByName.Where(cr => cr.MemberStatus.Value == _searchByStatus);
                        }
                    }
                    else if ((!String.IsNullOrEmpty(startDate) || !String.IsNullOrEmpty(endDate)) && !String.IsNullOrEmpty(searchByStatus) && String.IsNullOrEmpty(searchByName) && (!String.IsNullOrEmpty(searchByInterest) && searchByInterest != "All"))
                    {
                        MemberStatus _searchByStatus = MemberStatus.Yes;

                        if (searchByStatus == "Yes")
                        {
                            _searchByStatus = MemberStatus.Yes;
                            consumerRepsByDateTrainedByInterest = consumerRepsByDateTrainedByInterest.Where(cr => cr.MemberStatus.Value == _searchByStatus);
                        }
                        else if (searchByStatus == "No")
                        {
                            _searchByStatus = MemberStatus.No;
                            consumerRepsByDateTrainedByInterest = consumerRepsByDateTrainedByInterest.Where(cr => cr.MemberStatus.Value == _searchByStatus);
                        }
                    }
                    else if ((!String.IsNullOrEmpty(startDate) || !String.IsNullOrEmpty(endDate)) && !String.IsNullOrEmpty(searchByStatus) && !String.IsNullOrEmpty(searchByName) && (String.IsNullOrEmpty(searchByInterest) || searchByInterest == "All"))
                    {
                        MemberStatus _searchByStatus = MemberStatus.Yes;

                        if (searchByStatus == "Yes")
                        {
                            _searchByStatus = MemberStatus.Yes;
                            consumerRepsByDateTrainedByName = consumerRepsByDateTrainedByName.Where(cr => cr.MemberStatus.Value == _searchByStatus);
                        }
                        else if (searchByStatus == "No")
                        {
                            _searchByStatus = MemberStatus.No;
                            consumerRepsByDateTrainedByName = consumerRepsByDateTrainedByName.Where(cr => cr.MemberStatus.Value == _searchByStatus);
                        }
                    }
                    else if ((!String.IsNullOrEmpty(startDate) || !String.IsNullOrEmpty(endDate)) && String.IsNullOrEmpty(searchByStatus) && String.IsNullOrEmpty(searchByName) && (String.IsNullOrEmpty(searchByInterest) || searchByInterest == "All"))
                    {
                        MemberStatus _searchByStatus = MemberStatus.Yes;

                        if (searchByStatus == "Yes")
                        {
                            _searchByStatus = MemberStatus.Yes;
                            consumerRepsByDateTrained = consumerRepsByDateTrained.Where(cr => cr.MemberStatus.Value == _searchByStatus);
                        }
                        else if (searchByStatus == "No")
                        {
                            _searchByStatus = MemberStatus.No;
                            consumerRepsByDateTrained = consumerRepsByDateTrained.Where(cr => cr.MemberStatus.Value == _searchByStatus);
                        }
                    }
                    else if ((String.IsNullOrEmpty(startDate) || String.IsNullOrEmpty(endDate)) && !String.IsNullOrEmpty(searchByStatus) && !String.IsNullOrEmpty(searchByName) && (!String.IsNullOrEmpty(searchByInterest) && searchByInterest != "All"))
                    {
                        MemberStatus _searchByStatus = MemberStatus.Yes;

                        if (searchByStatus == "Yes")
                        {
                            _searchByStatus = MemberStatus.Yes;
                            consumerRepsByInterestByName = consumerRepsByInterestByName.Where(cr => cr.MemberStatus.Value == _searchByStatus);
                        }
                        else if (searchByStatus == "No")
                        {
                            _searchByStatus = MemberStatus.No;
                            consumerRepsByInterestByName = consumerRepsByInterestByName.Where(cr => cr.MemberStatus.Value == _searchByStatus);
                        }
                    }
                    else if ((String.IsNullOrEmpty(startDate) || String.IsNullOrEmpty(endDate)) && !String.IsNullOrEmpty(searchByStatus) && String.IsNullOrEmpty(searchByName) && (!String.IsNullOrEmpty(searchByInterest) && searchByInterest != "All"))
                    {
                        MemberStatus _searchByStatus = MemberStatus.Yes;

                        if (searchByStatus == "Yes")
                        {
                            _searchByStatus = MemberStatus.Yes;
                            consumerRepsByInterest = consumerRepsByInterest.Where(cr => cr.MemberStatus.Value == _searchByStatus);
                        }
                        else if (searchByStatus == "No")
                        {
                            _searchByStatus = MemberStatus.No;
                            consumerRepsByInterest = consumerRepsByInterest.Where(cr => cr.MemberStatus.Value == _searchByStatus);
                        }
                    }
                    else if ((String.IsNullOrEmpty(startDate) || String.IsNullOrEmpty(endDate)) && !String.IsNullOrEmpty(searchByStatus) && !String.IsNullOrEmpty(searchByName) && (String.IsNullOrEmpty(searchByInterest) || searchByInterest == "All"))
                    {
                        MemberStatus _searchByStatus = MemberStatus.Yes;

                        if (searchByStatus == "Yes")
                        {
                            _searchByStatus = MemberStatus.Yes;
                            consumerRepsByName = consumerRepsByName.Where(cr => cr.MemberStatus.Value == _searchByStatus);
                        }
                        else if (searchByStatus == "No")
                        {
                            _searchByStatus = MemberStatus.No;
                            consumerRepsByName = consumerRepsByName.Where(cr => cr.MemberStatus.Value == _searchByStatus);
                        }
                    }
                    else
                    {
                        MemberStatus _searchByStatus = MemberStatus.Yes;

                        if (searchByStatus == "Yes")
                        {
                            _searchByStatus = MemberStatus.Yes;
                            consumerReps = consumerReps.Where(cr => cr.MemberStatus.Value == _searchByStatus);
                        }
                        else if (searchByStatus == "No")
                        {
                            _searchByStatus = MemberStatus.No;
                            consumerReps = consumerReps.Where(cr => cr.MemberStatus.Value == _searchByStatus);
                        }
                    }
                    #endregion

                    if ((!String.IsNullOrEmpty(startDate) || !String.IsNullOrEmpty(endDate)) && !String.IsNullOrEmpty(searchByStatus) && !String.IsNullOrEmpty(searchByName) && (!String.IsNullOrEmpty(searchByInterest) && searchByInterest != "All"))
                    {
                        consumerRepsByDateTrainedByInterestByName = utils_ConRep.ComsumerReps_SortIndex(sortOrder, consumerRepsByDateTrainedByInterestByName);
                        viewModel.ConsumerRepModels = consumerRepsByDateTrainedByName.ToPagedList(pageNumber, pageSize);
                        return View(viewModel);
                    }
                    else if ((!String.IsNullOrEmpty(startDate) || !String.IsNullOrEmpty(endDate)) && !String.IsNullOrEmpty(searchByStatus) && String.IsNullOrEmpty(searchByName) && (!String.IsNullOrEmpty(searchByInterest) && searchByInterest != "All"))
                    {
                        consumerRepsByDateTrainedByInterest = utils_ConRep.ComsumerReps_SortIndex(sortOrder, consumerRepsByDateTrainedByInterest);
                        viewModel.ConsumerRepModels = consumerRepsByDateTrainedByInterest.ToPagedList(pageNumber, pageSize);
                        return View(viewModel);
                    }
                    else if ((!String.IsNullOrEmpty(startDate) || !String.IsNullOrEmpty(endDate)) && !String.IsNullOrEmpty(searchByStatus) && !String.IsNullOrEmpty(searchByName) && (String.IsNullOrEmpty(searchByInterest) || searchByInterest == "All"))
                    {
                        consumerRepsByDateTrainedByName = utils_ConRep.ComsumerReps_SortIndex(sortOrder, consumerRepsByDateTrainedByName);
                        viewModel.ConsumerRepModels = consumerRepsByDateTrainedByName.ToPagedList(pageNumber, pageSize);
                        return View(viewModel);
                    }
                    else if ((!String.IsNullOrEmpty(startDate) || !String.IsNullOrEmpty(endDate)) && !String.IsNullOrEmpty(searchByStatus) && String.IsNullOrEmpty(searchByName) && (String.IsNullOrEmpty(searchByInterest) || searchByInterest == "All"))
                    {
                        consumerRepsByDateTrained = utils_ConRep.ComsumerReps_SortIndex(sortOrder, consumerRepsByDateTrained);
                        viewModel.ConsumerRepModels = consumerRepsByDateTrained.ToPagedList(pageNumber, pageSize);
                        return View(viewModel);
                    }
                    else if ((String.IsNullOrEmpty(startDate) || String.IsNullOrEmpty(endDate)) && !String.IsNullOrEmpty(searchByStatus) && !String.IsNullOrEmpty(searchByName) && (!String.IsNullOrEmpty(searchByInterest) && searchByInterest != "All"))
                    {
                        consumerRepsByInterestByName = utils_ConRep.ComsumerReps_SortIndex(sortOrder, consumerRepsByInterestByName);
                        viewModel.ConsumerRepModels = consumerRepsByInterestByName.ToPagedList(pageNumber, pageSize);
                        return View(viewModel);
                    }
                    else if ((String.IsNullOrEmpty(startDate) || String.IsNullOrEmpty(endDate)) && !String.IsNullOrEmpty(searchByStatus) && String.IsNullOrEmpty(searchByName) && (!String.IsNullOrEmpty(searchByInterest) && searchByInterest != "All"))
                    {
                        consumerRepsByInterest = utils_ConRep.ComsumerReps_SortIndex(sortOrder, consumerRepsByInterest);
                        viewModel.ConsumerRepModels = consumerRepsByInterest.ToPagedList(pageNumber, pageSize);
                        return View(viewModel);
                    }
                    else if ((String.IsNullOrEmpty(startDate) || String.IsNullOrEmpty(endDate)) && !String.IsNullOrEmpty(searchByStatus) && !String.IsNullOrEmpty(searchByName) && (String.IsNullOrEmpty(searchByInterest) || searchByInterest == "All"))
                    {
                        consumerRepsByName = utils_ConRep.ComsumerReps_SortIndex(sortOrder, consumerRepsByName);
                        viewModel.ConsumerRepModels = consumerRepsByName.ToPagedList(pageNumber, pageSize);
                        return View(viewModel);
                    }
                    else
                    {
                        consumerReps = utils_ConRep.ComsumerReps_SortIndex(sortOrder, consumerReps);
                        viewModel.ConsumerRepModels = consumerReps.ToPagedList(pageNumber, pageSize);
                        return View(viewModel);
                    }
                }
                catch (Exception /*ex*/)
                {
                    int pageSize = 20;
                    int pageNumber = (viewModel.page ?? 1);

                    if (String.IsNullOrEmpty(searchByStatus))
                    {
                        searchByStatus = "Yes";
                    }

                    if (String.IsNullOrEmpty(searchByEndorseStatus))
                    {
                        searchByEndorseStatus = "Yes";
                    }

                    List<string> EndorseStatuses = new List<string>();
                    EndorseStatuses.Add("Yes");
                    EndorseStatuses.Add("No");
                    EndorseStatuses.Add("All");

                    ViewBag.EndorseStatuses = new SelectList(EndorseStatuses);

                    List<string> MemberStatusTypes = new List<string>();
                    MemberStatusTypes.Add("Yes");
                    MemberStatusTypes.Add("No");
                    MemberStatusTypes.Add("All");

                    ViewBag.MemberStatusTypes = new SelectList(MemberStatusTypes);

                    List<string> AreasOfInterest = new List<string>();
                    AreasOfInterest.Add("All");
                    foreach (var interest in db.ConsumerRepAreasOfInterest)
                    {
                        AreasOfInterest.Add(interest.AreaOfInterestName);
                    }
                    ViewBag.AreasOfInterest = new SelectList(AreasOfInterest);

                    ViewBag.NameSortParam = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
                    //ViewBag.MemberStatusSortParam = sortOrder == "MemberStatus" ? "memberstatus_desc" : "MemberStatus";

                    var consumerReps = from cr in db.ConsumerReps
                                       select cr;

                    consumerReps = utils_ConRep.ComsumerReps_SortIndex(sortOrder, consumerReps);
                    viewModel.ConsumerRepModels = consumerReps.ToPagedList(pageNumber, pageSize);
                    return View(viewModel);
                }
            }
            else
            {
                viewModel = new ConsumerRepIndexViewModel();

                int pageSize = 20;
                int pageNumber = (viewModel.page ?? 1);


                searchByStatus = "Yes";
                searchByEndorseStatus = "Yes";
                searchByInterest = "All";
                searchByName = "";
                startDate = "";
                endDate = ""; 

                List<string> EndorseStatuses = new List<string>();
                EndorseStatuses.Add("Yes");
                EndorseStatuses.Add("No");
                EndorseStatuses.Add("All");

                ViewBag.EndorseStatuses = new SelectList(EndorseStatuses);

                List<string> MemberStatusTypes = new List<string>();
                MemberStatusTypes.Add("Yes");
                MemberStatusTypes.Add("No");
                MemberStatusTypes.Add("All");

                ViewBag.MemberStatusTypes = new SelectList(MemberStatusTypes);

                List<string> AreasOfInterest = new List<string>();
                AreasOfInterest.Add("All");
                foreach (var interest in db.ConsumerRepAreasOfInterest)
                {
                    AreasOfInterest.Add(interest.AreaOfInterestName);
                }
                ViewBag.AreasOfInterest = new SelectList(AreasOfInterest);

                ViewBag.NameSortParam = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
                //ViewBag.MemberStatusSortParam = sortOrder == "MemberStatus" ? "memberstatus_desc" : "MemberStatus";

                var consumerReps = from cr in db.ConsumerReps
                                   select cr;

                consumerReps = utils_ConRep.ComsumerReps_SortIndex(sortOrder, consumerReps);
                viewModel.ConsumerRepModels = consumerReps.ToPagedList(pageNumber, pageSize);
                return View(viewModel); 
            }
        }

        // GET: /ConsumerRepModel/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ConsumerRepModel consumerrepmodel = await db.ConsumerReps.FindAsync(id);
            if (consumerrepmodel == null)
            {
                return HttpNotFound();
            }
            return View(consumerrepmodel);
        }

        // GET: /ConsumerRepModel/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /ConsumerRepModel/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="FirstName,LastName,MemberStatus,DateTrained")] ConsumerRepModel consumerrepmodel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    consumerrepmodel.EndorsementStatus = EndorsementStatus.InActive;
                    db.ConsumerReps.Add(consumerrepmodel);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException /* dex */)
            {
                // Log the error here with dex var
                ModelState.AddModelError("", "Unable to save changes. Please try again.");
            }
            return View(consumerrepmodel);
        }

        // GET: /ConsumerRepModel/AddToInterest/id
        public async Task<ActionResult> AddToInterest(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var vm = new AddToInterestConsumerRepViewModel
            {
                ConsumerRepAreaOfInterestModel = new ConsumerRepAreaOfInterestModel(),
                ConsumerRepModel = db.ConsumerReps.Find(id),
                ConsumerRepAreaOfInterests = new SelectList(db.ConsumerRepAreasOfInterest, "ConsumerRepAreaOfInterestModelID", "AreaOfInterestName")
            };

            if (vm.ConsumerRepModel == null)
            {
                return HttpNotFound();
            }
            return View(vm);
        }

        // GET: /ConsumerRepModel/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ConsumerRepModel consumerrepmodel = await db.ConsumerReps.FindAsync(id);
            if (consumerrepmodel == null)
            {
                return HttpNotFound();
            }

            return View(consumerrepmodel);
        }

        // POST: /ConsumerRepModel/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="ConsumerRepModelID,FirstName,LastName,MemberStatus,EndorsementStatus,DateTrained")] ConsumerRepModel consumerrepmodel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(consumerrepmodel).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException /* dex */)
            {
                // Log the error here with dex var
                ModelState.AddModelError("", "Unable to save changes. Please try again.");
            }
            return View(consumerrepmodel);
        }

        // GET: /ConsumerRepModel/Delete/5
        public async Task<ActionResult> Delete(int? id, bool? saveChangesError=false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if(saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete failed. Please Try Again";
            }
            ConsumerRepModel consumerrepmodel = await db.ConsumerReps.FindAsync(id);
            if (consumerrepmodel == null)
            {
                return HttpNotFound();
            }
            return View(consumerrepmodel);
        }

        // POST: /ConsumerRepModel/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                ConsumerRepModel consumerrepmodel = await db.ConsumerReps.FindAsync(id);
                db.ConsumerReps.Remove(consumerrepmodel);
                await db.SaveChangesAsync();
            }
            catch (DataException/* dex */)
            {
                //Log the error 
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
