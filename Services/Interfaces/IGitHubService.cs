using TaskSH.Models;

namespace TaskSH.Services.Interfaces
{
    public interface IGitHubService
    {
        public Task<List<GitHubData>> GetAllGitHubDataAsync();

        public GitHubData? GetGitHubRepoById(string id);

        public List<GitHubData>? EditGitHubRepo(GitHubData? gitHubData);
    }
}
