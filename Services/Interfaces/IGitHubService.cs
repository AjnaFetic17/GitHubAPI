using TaskSH.Models;

namespace TaskSH.Services.Interfaces
{
    public interface IGitHubService
    {
        public Task<List<GitHubData>> GetGitHubDataAsync();

        public GitHubData? GetGitHubRepo(string id);

        public GitHubData? EditGitHubRepo(GitHubData? gitHubData);
    }
}
