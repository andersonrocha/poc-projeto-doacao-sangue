using System.Web;
using System.Web.Mvc;
using DoacaoSangue.Web.Infra;

namespace DoacaoSangue.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new CustomHandleErrorAttribute());
        }
    }
}
