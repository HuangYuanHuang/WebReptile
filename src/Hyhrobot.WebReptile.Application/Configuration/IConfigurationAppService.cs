using System.Threading.Tasks;
using Hyhrobot.WebReptile.Configuration.Dto;

namespace Hyhrobot.WebReptile.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
