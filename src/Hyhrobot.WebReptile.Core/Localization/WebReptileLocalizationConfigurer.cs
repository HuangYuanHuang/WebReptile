using Abp.Configuration.Startup;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Reflection.Extensions;

namespace Hyhrobot.WebReptile.Localization
{
    public static class WebReptileLocalizationConfigurer
    {
        public static void Configure(ILocalizationConfiguration localizationConfiguration)
        {
            localizationConfiguration.Sources.Add(
                new DictionaryBasedLocalizationSource(WebReptileConsts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        typeof(WebReptileLocalizationConfigurer).GetAssembly(),
                        "Hyhrobot.WebReptile.Localization.SourceFiles"
                    )
                )
            );
        }
    }
}
