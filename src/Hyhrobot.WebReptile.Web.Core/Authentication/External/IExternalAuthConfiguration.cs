using System.Collections.Generic;

namespace Hyhrobot.WebReptile.Authentication.External
{
    public interface IExternalAuthConfiguration
    {
        List<ExternalLoginProviderInfo> Providers { get; }
    }
}
