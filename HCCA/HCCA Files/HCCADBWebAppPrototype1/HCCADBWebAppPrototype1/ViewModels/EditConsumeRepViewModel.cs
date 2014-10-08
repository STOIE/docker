using HCCADBWebAppPrototype1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.ComponentModel.DataAnnotations;

namespace HCCADBWebAppPrototype1.ViewModels
{
    public class EditConsumeRepViewModel
    {

        public ConsumerRepModel ConsumerRep { get; set; }

        public List<SelectListItem> MemberStatusOptions { get; set; }

        [Required(ErrorMessage = "Member Status is required")]
        public string MemberStatus { get; set; }
    }
}