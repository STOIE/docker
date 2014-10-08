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
    public enum EndorsementType { Organisational, Standard };

    public class ConsumerRepCommitteeHistoryModel
    {
        [Display(Name = "Committee Endorsement History")]
        public int ConsumerRepCommitteeHistoryModelID { get; set; }

        [Display(Name = "Committee")]
        public int CommitteeModelID { get; set; }

        [Display(Name = "Comsumer Rep")]
        public int ConsumerRepModelID { get; set; }

        [Display(Name = "Preperation Time")]
        public int PrepTime { get; set; }

        [Display(Name = "Meeting Time")]
        public int Meetingtime { get; set; }

        [Display(Name = "Date Added to DB")]
        [DataType(DataType.DateTime)]
        public DateTime ReportedDate { get; set;  }

        [Display(Name = "Endorsement Date")]
        [DataType(DataType.DateTime)]
        public DateTime EndorsementDate { get; set; }

        [Display(Name = "Date Committee Ended")]
        [DataType(DataType.DateTime)]
        public DateTime? FinishedDate { get; set; }

        [Display(Name = "Endorsement Type")] 
        public EndorsementType? EndorsementType { get; set; }

        public virtual ConsumerRepModel ConsumerRepModel { get; set; }

        public virtual CommitteeModel CommitteeModel { get; set; }
    }
}