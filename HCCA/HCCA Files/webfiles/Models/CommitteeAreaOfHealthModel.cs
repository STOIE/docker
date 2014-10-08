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
    public class CommitteeAreaOfHealthModel
    {
        public int CommitteeAreaOfHealthModelID { get; set; }

        [Required]
        [StringLength(64)]
        [DataType(DataType.Text)]
        [Display(Name="Area Of Health")]
        public string AreaOfHealthName { get; set; }

        public virtual ICollection<CommitteeModel_CommitteeAreaOfHealthModel> CommitteeModel_CommitteeAreaOfHealthModels { get; set; }

    }
}