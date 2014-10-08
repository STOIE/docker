using HCCADBWebAppPrototype1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace HCCADBWebAppPrototype1.ViewModels
{
    public class ConsumerRepIndexViewModel
    {
        public IPagedList<ConsumerRepModel> ConsumerRepModels { get; set; }
        public int? page { get; set; }
    }
}