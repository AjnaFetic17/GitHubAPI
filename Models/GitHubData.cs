namespace TaskSH.Models
{
    public class GitHubData
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Owner { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public string Visibility { get; set; } = string.Empty;  
        public string Language { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}
