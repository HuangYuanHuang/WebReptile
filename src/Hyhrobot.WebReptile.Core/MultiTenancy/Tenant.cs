using Abp.MultiTenancy;
using Hyhrobot.WebReptile.Authorization.Users;

namespace Hyhrobot.WebReptile.MultiTenancy
{
    public class Tenant : AbpTenant<User>
    {
        public Tenant()
        {            
        }

        public Tenant(string tenancyName, string name)
            : base(tenancyName, name)
        {
        }
    }
}
