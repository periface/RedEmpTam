using Abp.Web.Mvc.Views;

namespace MercadoCinotam.Web.Views
{
    public abstract class MercadoCinotamWebViewPageBase : MercadoCinotamWebViewPageBase<dynamic>
    {

    }

    public abstract class MercadoCinotamWebViewPageBase<TModel> : AbpWebViewPage<TModel>
    {
        protected MercadoCinotamWebViewPageBase()
        {
            LocalizationSourceName = MercadoCinotamConsts.LocalizationSourceName;
        }
    }
}