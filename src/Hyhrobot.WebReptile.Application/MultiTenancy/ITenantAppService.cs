using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Hyhrobot.WebReptile.MultiTenancy.Dto;

namespace Hyhrobot.WebReptile.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}
