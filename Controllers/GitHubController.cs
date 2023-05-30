using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TaskSH.Models;
using TaskSH.Models.Helpers;
using TaskSH.Services.Interfaces;

namespace TaskSH.Controllers
{
    [Authorize]
    public class GitHubController : Controller
    {

        private readonly IGitHubService _gitHubService;
        private readonly ICacheService _cacheService;
        public GitHubController(IGitHubService gitHubService, ICacheService cacheService)
        {

            _gitHubService = gitHubService;
            _cacheService = cacheService;
        }

        public async Task<IActionResult> GitHubApiCallAsync()
        {
            try
            {
                var cacheResult = _cacheService.GetFromCache(CacheKeys.GitHub);
                if (cacheResult != null)
                {
                    return View(cacheResult);
                }
                else
                {
                    ViewBag.Message = "send";
                    return View(await _gitHubService.GetAllGitHubDataAsync());
                }
            }
            catch (Exception ex)
            {
                return View("Error", new CustomError()
                {
                    Message = ex.Message,
                });
            }
        }

        [HttpPost]
        public ActionResult SaveGitHubData(List<GitHubData> data)
        {
            try
            {
                var jsonFile = "GitHubData.json";
                if (!System.IO.File.Exists(jsonFile))
                {
                    using var fileStream = new FileStream(jsonFile, FileMode.Create);
                }

                var serializer = new JsonSerializer
                {
                    Formatting = Formatting.Indented
                };

                using var streamWriter = new StreamWriter(jsonFile);
                using JsonWriter writer = new JsonTextWriter(streamWriter);
                serializer.Serialize(writer, data);
            }
            catch (Exception ex)
            {
                return View("Error", new CustomError()
                {
                    Message = ex.Message,
                });
            }

            ViewBag.Message = "";

            return View("GitHubApiCall", data);
        }

        [HttpGet("{id}")]
        public ActionResult Edit(string id)
        {
            try
            {
                var cacheResult = _cacheService.GetFromCache(CacheKeys.GitHub);
                if (cacheResult != null)
                {
                    return View("EditGitHubInfo", _gitHubService.GetGitHubRepoById(id));
                }
                else
                {
                    ViewBag.Message = "send";
                    return View("GitHubApiCall");
                }
            }
            catch (Exception ex)
            {
                return View("Error", new CustomError()
                {
                    Message = ex.Message,
                });
            }
        }

        [HttpPost]
        public ActionResult EditGitHubData(GitHubData data)
        {
            try
            {
                var result = _gitHubService.EditGitHubRepo(data);

                if (result != null)
                {
                    return View("GitHubApiCall", result);
                }

                return View("Error", "Something unexpected happened.");

            }
            catch (Exception ex)
            {
                return View("Error", new CustomError()
                {
                    Message = ex.Message,
                });
            }
        }
    }
}
