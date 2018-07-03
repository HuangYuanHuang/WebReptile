using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Hyhrobot.WebReptile.Configuration;
using Hyhrobot.WebReptile.Web;

namespace Hyhrobot.WebReptile.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class WebReptileDbContextFactory : IDesignTimeDbContextFactory<WebReptileDbContext>
    {
        public WebReptileDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<WebReptileDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            WebReptileDbContextConfigurer.Configure(builder, configuration.GetConnectionString(WebReptileConsts.ConnectionStringName));

            return new WebReptileDbContext(builder.Options);
        }
    }
}
