using CV_Site;
using IServise;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Octokit;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CvSiteController : ControllerBase
    {
        public readonly IGitHubService _service;
        public CvSiteController(IGitHubService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("GetPortfolio")]
        public Task<Portfolio> GetPortfolio()
        {
            return _service.GetPortfolio();
        }

        [HttpGet]
        [Route("SearchRepositories")]
        public Task<List<Repository>> SearchRepositories(string? userName, string? repoName, Language? lang)
        {
            return _service.SearchRepositories(userName, repoName, lang);
        }
    }
}
