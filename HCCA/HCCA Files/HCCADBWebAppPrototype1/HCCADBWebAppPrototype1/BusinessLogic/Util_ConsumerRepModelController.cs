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
    public class Util_ConsumerRepModelController
    {
        public IQueryable<ConsumerRepModel> ComsumerReps_SortIndex(string sortOrder, IQueryable<ConsumerRepModel> store)
        {
            switch (sortOrder)
            {
                case "name_desc":
                    store = store.OrderByDescending(cr => cr.LastName);
                    break;
                case "MemberStatus":
                    store = store.OrderBy(cr => cr.MemberStatus);
                    break;
                case "memberstatus_desc":
                    store = store.OrderByDescending(cr => cr.MemberStatus);
                    break;
                default:
                    store = store.OrderBy(cr => cr.LastName);
                    break;
            }
            return store;
        }
    }
}