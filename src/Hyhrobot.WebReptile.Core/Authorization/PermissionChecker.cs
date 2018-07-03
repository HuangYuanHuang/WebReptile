using Abp.Authorization;
using Hyhrobot.WebReptile.Authorization.Roles;
using Hyhrobot.WebReptile.Authorization.Users;

namespace Hyhrobot.WebReptile.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
