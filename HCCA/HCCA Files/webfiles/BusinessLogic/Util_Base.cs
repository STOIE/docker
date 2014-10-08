using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HCCADBWebAppPrototype1.Models;
using HCCADBWebAppPrototype1.ViewModels;
using HCCADBWebAppPrototype1.DAL;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.SqlServer;


namespace HCCADBWebAppPrototype1.BusinessLogic
{
    public class Util_Base
    {

        public string GetCurrentDate(int opt)
        {
            switch (opt)
            {
                case 0:
                    return DateTime.Now.ToString("yyyy");
                case 1:
                    return DateTime.Now.ToString("MM-yyyy");
                case 2:
                    return DateTime.Now.ToString("dd-MM-yyyy");
                default:
                    return DateTime.Now.ToString("dd-MM-yyyy");
            }
        }
    }
}