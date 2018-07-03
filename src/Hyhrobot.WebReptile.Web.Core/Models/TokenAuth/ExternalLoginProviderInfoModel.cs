using Abp.AutoMapper;
using Hyhrobot.WebReptile.Authentication.External;

namespace Hyhrobot.WebReptile.Models.TokenAuth
{
    [AutoMapFrom(typeof(ExternalLoginProviderInfo))]
    public class ExternalLoginProviderInfoModel
    {
        public string Name { get; set; }

        public string ClientId { get; set; }
    }
}
