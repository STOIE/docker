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
    public class CommitteeModel_CommitteeAreaOfHealthModel
    {
        [Display(Name="Committee Area Of Health")]
        public int CommitteeModel_CommitteeAreaOfHealthModelID { get; set; }

        [Display(Name="Committee")]
        public int CommitteeModelID { get; set; }

        [Display(Name="Committee Area Of Health")]
        public int CommitteeAreaOfHealthModelID { get; set; }

        public virtual CommitteeModel CommitteeModel { get; set; }

        public virtual CommitteeAreaOfHealthModel CommitteeAreaOfHealthModel { get; set; }
    
    }
}