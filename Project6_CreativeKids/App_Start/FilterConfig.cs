using System.Web;
using System.Web.Mvc;

namespace Project6_CreativeKids
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
