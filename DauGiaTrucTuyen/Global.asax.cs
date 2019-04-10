using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using static DauGiaTrucTuyen.Areas.Admin.Models.ManagerUserViewModel;
using DauGiaTrucTuyen.Areas.Admin.Models;
using DauGiaTrucTuyen.Data;

namespace DauGiaTrucTuyen
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AutoMapper.Mapper.Initialize(conf =>
            {
                conf.CreateMap<DetailUserViewModel, Microsoft.AspNet.Identity.EntityFramework.IdentityUser>();
                conf.CreateMap<ListUserViewModel, Microsoft.AspNet.Identity.EntityFramework.IdentityUser>();
                conf.CreateMap<DetailCategoryViewModel, Category>();
                conf.CreateMap<AddCategoryViewModel, Category>();
                conf.CreateMap<EditCategoryViewModel, Category>();
                conf.CreateMap<ListProductViewModel, Product>();
                conf.CreateMap<AddReportViewModel, Report>();
            });
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
