using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace Hyhrobot.WebReptile.EntityFrameworkCore
{
    public static class WebReptileDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<WebReptileDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<WebReptileDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
