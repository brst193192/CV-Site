using CV_Site;
using IServise;
using Microsoft.Extensions.Caching.Memory;
using Octokit;

namespace Api.CachedServices
{
    public class CachedGitHubService : IGitHubService
    {
        private readonly IGitHubService _gitHubService;
        private readonly IMemoryCache _memoryCache;
        private const string UserPortfolioKey = "UserPortfolioKey";
        public CachedGitHubService(IGitHubService gitHubService, IMemoryCache memoryCache)
        {
            _gitHubService = gitHubService;
            _memoryCache = memoryCache;
        }

        public async Task<Portfolio> GetPortfolio()
        {
            if (_memoryCache.TryGetValue(UserPortfolioKey, out (Portfolio portfolio, DateTime savedTime)? cacheEntry))
            {
                // last updated in github
                var currentUpdateTime = await _gitHubService.GetLastUpdateTime();//await _gitHubService.get();
                //if last update is match or older than the cache entry, return cached portfolio
                if (cacheEntry!.Value.savedTime >= currentUpdateTime)
                    return cacheEntry.Value.portfolio;
            }

            var portfolio = await _gitHubService.GetPortfolio();
            // לא להגדיר תאריך תפוגה קבוע, אלא רק לשמור את המידע
            _memoryCache.Set(UserPortfolioKey, (portfolio, DateTime.UtcNow));
            return portfolio;
        }

        public async Task<List<Repository>> SearchRepositories(string? userName, string? repoName, Language? lang)
        {
            return await _gitHubService.SearchRepositories(userName, repoName, lang);
        }

        public Task<DateTime> GetLastUpdateTime()
        {
            throw new NotImplementedException();
        }
    }
}
