using IServise;
using Microsoft.Extensions.Options;
using Octokit;
namespace CV_Site
{
    public class GitHubService : IGitHubService
    {
        private readonly GitHubClient _client;
        private readonly GitHubIntegrationOptions _options;
        private readonly string URL = "https://github.com/";

        public GitHubService(IOptions<GitHubIntegrationOptions> options)
        {
            _options = options.Value;
            _client = new GitHubClient(new ProductHeaderValue("my-github-app"))
            {
                Credentials = new Credentials(_options.Token)
            };
        }
       
        public async Task<List<Repository>> SearchRepositories(string? userName, string? repoName, Language? lang)
        {
            var request = new SearchRepositoriesRequest(repoName) { Language = lang, User = userName };
            var result = await _client.Search.SearchRepo(request);
            return result.Items.ToList();
        }
        
        public async Task<Portfolio> GetPortfolio()
        {
            var portfolio = new Portfolio();
            portfolio.UserName = _options.UserName!;
            portfolio.Repositories = (await _client.Repository.GetAllForUser(_options.UserName)).ToList();
            return portfolio;
        }
        
        public async Task<DateTime> GetLastUpdateTime()
        {
            var activities = await _client.Activity.Events.GetAllUserPerformed(_options.UserName);
            DateTime latestUpdateTime = DateTime.MinValue;

            if (activities.Any())
            {
                latestUpdateTime = activities.Max(a => a.CreatedAt.UtcDateTime); // זמן הפעילות האחרונה
            }
            return latestUpdateTime;
        }
    }
}
