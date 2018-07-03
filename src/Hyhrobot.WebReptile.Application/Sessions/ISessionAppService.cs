using System.Threading.Tasks;
using Abp.Application.Services;
using Hyhrobot.WebReptile.Sessions.Dto;

namespace Hyhrobot.WebReptile.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
