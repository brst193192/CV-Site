using Octokit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CV_Site
{
    public class Portfolio
    {
        public string? UserName { get; set; }
        public List<Repository> Repositories { get; set; } = null!;
    }
}
