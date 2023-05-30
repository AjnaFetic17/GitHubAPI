using Newtonsoft.Json;
using System.Dynamic;
using System.Net.Http.Headers;
using TaskSH.Models;
using TaskSH.Models.Helpers;
using TaskSH.Services.Interfaces;

namespace TaskSH.Services
{
    public class GitHubService : IGitHubService
    {
        private readonly IConfiguration _configuration;
        private readonly ICacheService _cacheService;

        public GitHubService(IConfiguration configuration, ICacheService cacheService)
        {
            _configuration = configuration;
            _cacheService = cacheService;
        }

        public GitHubData? GetGitHubRepoById(string id)
        {
            var items = (List<GitHubData>)_cacheService.GetFromCache(CacheKeys.GitHub);
            if(items != null)
            {
                return items.Find(x => x.Id == id);
            }
            else
            {
                return null;
            }
        }

        public async Task<List<GitHubData>> GetAllGitHubDataAsync()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(AuthType.Bearer, _configuration.GetSection("Authentication:GitHub:Bearer").Value);
            var commentValue = new ProductInfoHeaderValue(Consts.GitHubUserAgent);

            client.DefaultRequestHeaders.UserAgent.Add(commentValue);
            var result = await client.GetAsync(Consts.GitHubUrl);

            var response = await result.Content.ReadAsStringAsync();

            if (result.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception(response);
            }
           
            if (response == null)
            {
                return new List<GitHubData>();
            }

            List<ExpandoObject> res = JsonConvert.DeserializeObject<List<ExpandoObject>>(response)!;

            var data = res.Select(item =>
            {
                IDictionary<string, object> propertyValues = item!;

                return new GitHubData()
                {
                    Id = propertyValues["id"].ToString()!,
                    Name = propertyValues["name"].ToString()!,
                    Language = propertyValues["language"] != null ? propertyValues["language"].ToString()! : "Not specified",
                    Owner = ((ExpandoObject)propertyValues["owner"]).ElementAt(0).Value!.ToString()!,
                    Url = propertyValues["html_url"].ToString()!,
                    Visibility = propertyValues["visibility"].ToString()!,
                };
            }).ToList();

            _cacheService.AddToCache(CacheKeys.GitHub, data);

            return data;
        }

        public List<GitHubData>? EditGitHubRepo(GitHubData? gitHubData)
        {
            try
            {
                var jsonFile = "changedValue.json";
                if (!File.Exists(jsonFile))
                {
                    using var fileStream = new FileStream(jsonFile, FileMode.Create);
                }

                var serializer = new JsonSerializer
                {
                    Formatting = Formatting.Indented
                };
              
                var items = (List<GitHubData>)_cacheService.GetFromCache(CacheKeys.GitHub);
                if (items != null)
                {
                    items.ElementAt(items.FindIndex(x => x.Id == gitHubData.Id)).Description = gitHubData.Description;
                    _cacheService.AddToCache(CacheKeys.GitHub, items);
                    using var streamWriter = new StreamWriter(jsonFile);
                    using JsonWriter writer = new JsonTextWriter(streamWriter);
                    serializer.Serialize(writer, items);

                    return items;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}
