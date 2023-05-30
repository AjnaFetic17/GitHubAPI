using WebApplication2.Models;

namespace WebApplication2.Services.Interfaces
{
    public interface IGitHubService
    {
        public Task<List<GitHubData>> GetGitHubDataAsync();

        public GitHubData? GetGitHubRepo(string id);

        public GitHubData? EditGitHubRepo(GitHubData? gitHubData);
    }
}
