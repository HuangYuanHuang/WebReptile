using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using Hyhrobot.WebReptile.Configuration.Dto;

namespace Hyhrobot.WebReptile.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : WebReptileAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
