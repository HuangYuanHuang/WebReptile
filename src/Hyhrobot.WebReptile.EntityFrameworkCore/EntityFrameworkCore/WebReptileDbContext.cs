using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using Hyhrobot.WebReptile.Authorization.Roles;
using Hyhrobot.WebReptile.Authorization.Users;
using Hyhrobot.WebReptile.MultiTenancy;

namespace Hyhrobot.WebReptile.EntityFrameworkCore
{
    public class WebReptileDbContext : AbpZeroDbContext<Tenant, Role, User, WebReptileDbContext>
    {
        /* Define a DbSet for each entity of the application */
        
        public WebReptileDbContext(DbContextOptions<WebReptileDbContext> options)
            : base(options)
        {
        }
    }
}
