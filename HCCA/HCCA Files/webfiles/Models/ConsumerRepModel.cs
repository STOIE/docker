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
    public enum EndorsementStatus { Active, InActive };
    public enum MemberStatus { Yes, No };

    public class ConsumerRepModel
    {
        public int ConsumerRepModelID { get; set; }

        [Required]
        [StringLength(32)]
        [DataType(DataType.Text)] 
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(32)]
        [DataType(DataType.Text)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Endorsement Status")] 
        public EndorsementStatus? EndorsementStatus { get; set; }

        [Required]
        [Display(Name = "Member Status")] 
        public MemberStatus? MemberStatus { get; set; }

        [Required]
        [Display(Name = "Date Trained")] // month - year
        [DataType(DataType.DateTime)]
        public DateTime DateTrained { get; set; }
        
        public string FullName
        {
            get { return (FirstName + " " + LastName); }
        }


        public virtual ICollection<ConsumerRepModel_ConsumerRepAreaOfInterestModel> ConsumerRepModel_ConsumerRepAreasOfInterestModels { get; set; }

        public virtual ICollection<ConsumerRepCommitteeHistoryModel> ConsumerRepsCommitteeHistoryModels { get; set; }

    }
}