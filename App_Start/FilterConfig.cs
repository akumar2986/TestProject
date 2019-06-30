using System.Web;
using System.Web.Mvc;

namespace TEST
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new TestErrorAttribute());
        }
        //public static void ConfigrationManager(GlobalFilterCollection filters)
        //{
        //    filters.Add(new HandleErrorAttribute());
        //}
    }
}
