using HCCADBWebAppPrototype1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HCCADBWebAppPrototype1.ViewModels
{
    public class CreateCommitteeViewModel
    {
        public CommitteeModel NewCommitteeModel { get; set; }

        public ConsumerRepCommitteeHistoryModel NewConsumerRepCommitteeHistoryModel { get; set; }

        public CommitteeModel_CommitteeAreaOfHealthModel NewCommitteeAreaOfHealthModel { get; set; }

        public IEnumerable<SelectListItem> ConsumerRepsID { get; set; }

        public IEnumerable<SelectListItem> CommitteeAreasOfHealthID { get; set; }
    }
}