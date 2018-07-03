using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Hyhrobot.WebReptile.Authorization;

namespace Hyhrobot.WebReptile
{
    [DependsOn(
        typeof(WebReptileCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class WebReptileApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<WebReptileAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(WebReptileApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddProfiles(thisAssembly)
            );
        }
    }
}
