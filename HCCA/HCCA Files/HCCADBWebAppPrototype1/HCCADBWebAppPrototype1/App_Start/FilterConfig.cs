using System.Web;
using System.Web.Mvc;

namespace HCCADBWebAppPrototype1
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
