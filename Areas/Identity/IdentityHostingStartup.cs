using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(ClayTestCase.Areas.Identity.IdentityHostingStartup))]
namespace ClayTestCase.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}