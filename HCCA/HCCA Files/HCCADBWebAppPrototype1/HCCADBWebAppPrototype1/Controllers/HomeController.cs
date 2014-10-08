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
using HCCADBWebAppPrototype1.ViewModels;
using HCCADBWebAppPrototype1.DAL;
using PagedList;


namespace HCCADBWebAppPrototype1.Controllers
{
    public class HomeController : Controller
    {
        private HCCADatabaseContext db = new HCCADatabaseContext();

        public ActionResult Index(MainViewModel viewModel)
        {
            int pageSize = 10;

            int consumerPageNumber = (viewModel.consumerPage ?? 1);
            int committeePageNumber = (viewModel.committeePage ?? 1);

            var consumers = from cr in db.ConsumerReps
                            select cr;

            consumers = consumers.OrderBy(cr => cr.LastName);

            var committees = from com in db.Committees
                             select com;

            committees = committees.OrderBy(com => com.CommitteeName);

            viewModel.ConsumerRepModels = consumers.ToPagedList(consumerPageNumber, pageSize);
            viewModel.CommitteeModels = committees.ToPagedList(committeePageNumber, pageSize);

            return View(viewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}