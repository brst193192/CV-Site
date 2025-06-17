using CV_Site;
using Octokit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IServise
{
    public interface IGitHubService
    {
        Task<List<Repository>> SearchRepositories(string? userName, string? repoName, Language? lang);
        
        Task<Portfolio> GetPortfolio();

        Task<DateTime> GetLastUpdateTime();
    }
}
