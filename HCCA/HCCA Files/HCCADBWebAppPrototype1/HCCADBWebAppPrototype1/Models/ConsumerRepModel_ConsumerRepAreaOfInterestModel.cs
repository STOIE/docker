using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HCCADBWebAppPrototype1.Models
{
    public class ConsumerRepModel_ConsumerRepAreaOfInterestModel
    {
        [Display(Name = "Consumer Rep Area of Interest")]
        public int ConsumerRepModel_ConsumerRepAreaOfInterestModelID { get; set; }

        [Display(Name = "Consumer Rep")]
        public int ConsumerRepModelID { get; set; }

        [Display(Name = "Consumer Rep Area of Interest")]
        public int ConsumerRepAreaOfInterestModelID { get; set; }

        public virtual ConsumerRepModel ConsumerRepModel { get; set; }

        public virtual ConsumerRepAreaOfInterestModel ConsumerRepAreaOfInterestModel { get; set; }

    }
}