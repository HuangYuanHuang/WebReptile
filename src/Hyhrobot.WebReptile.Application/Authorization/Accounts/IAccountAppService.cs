using System.Threading.Tasks;
using Abp.Application.Services;
using Hyhrobot.WebReptile.Authorization.Accounts.Dto;

namespace Hyhrobot.WebReptile.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
