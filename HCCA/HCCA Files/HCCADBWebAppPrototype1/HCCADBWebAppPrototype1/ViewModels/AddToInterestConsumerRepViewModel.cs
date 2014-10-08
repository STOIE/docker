using HCCADBWebAppPrototype1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HCCADBWebAppPrototype1.ViewModels
{
    public class AddToInterestConsumerRepViewModel
    {
        public ConsumerRepModel ConsumerRepModel { get; set; }

        public ConsumerRepAreaOfInterestModel ConsumerRepAreaOfInterestModel { get; set; }

        public IEnumerable<SelectListItem> ConsumerRepAreaOfInterests { get; set; }

    }
}