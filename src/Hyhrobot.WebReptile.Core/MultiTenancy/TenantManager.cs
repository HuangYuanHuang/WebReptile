using Abp.Application.Features;
using Abp.Domain.Repositories;
using Abp.MultiTenancy;
using Hyhrobot.WebReptile.Authorization.Users;
using Hyhrobot.WebReptile.Editions;

namespace Hyhrobot.WebReptile.MultiTenancy
{
    public class TenantManager : AbpTenantManager<Tenant, User>
    {
        public TenantManager(
            IRepository<Tenant> tenantRepository, 
            IRepository<TenantFeatureSetting, long> tenantFeatureRepository, 
            EditionManager editionManager,
            IAbpZeroFeatureValueStore featureValueStore) 
            : base(
                tenantRepository, 
                tenantFeatureRepository, 
                editionManager,
                featureValueStore)
        {
        }
    }
}
