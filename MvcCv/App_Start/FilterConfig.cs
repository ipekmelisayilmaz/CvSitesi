using System.Web;
using System.Web.Mvc;

namespace MvcCv
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new AuthorizeAttribute()); // Tüm projede Authorize uygulanır.
            filters.Add(new HandleErrorAttribute()); // Hata yönetimi filtresi.
           
        }
    }
}
