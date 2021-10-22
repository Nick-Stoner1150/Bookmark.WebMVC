using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Bookmark.WebMVC.Startup))]
namespace Bookmark.WebMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
