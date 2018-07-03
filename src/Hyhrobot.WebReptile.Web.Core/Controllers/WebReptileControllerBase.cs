using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace Hyhrobot.WebReptile.Controllers
{
    public abstract class WebReptileControllerBase: AbpController
    {
        protected WebReptileControllerBase()
        {
            LocalizationSourceName = WebReptileConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
