using IServise;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CV_Site
{
    public static class Extensions
    {
        public static void AddGitHubIntegration(this IServiceCollection services, Action<GitHubIntegrationOptions> configureOptions)
        {
            services.Configure(configureOptions);
            services.AddScoped<IGitHubService, GitHubService>();
        }
    }
}
