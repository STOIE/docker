using HCCADBWebAppPrototype1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace HCCADBWebAppPrototype1.ViewModels
{
    public class CommitteeIndexViewModel
    {
        public IPagedList<CommitteeModel> CommitteeModels { get; set; }
        public int? page { get; set; }
    }
}