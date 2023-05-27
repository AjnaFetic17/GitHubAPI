using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net;
using System.Net.Http.Headers;
using Tweetinvi;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<IActionResult> Index()
        {
            //    var _twitterClient = new TwitterClient(_configuration.GetSection("Authentication:Twitter:ApiKey").Value,
            //        _configuration.GetSection("Authentication:Twitter:ApiKeySecret").Value,
            //        _configuration.GetSection("Authentication:Twitter:AccessToken").Value,
            //        _configuration.GetSection("Authentication:Twitter:AccessTokenSecret").Value);
            //_twitterClient = new TwitterClient(apiKey, apiSecret, accessToken, accessSecret);

            //OAuthRequest client = OAuthRequest.ForProtectedResource("GET", _configuration.GetSection("Authentication:Twitter:ClientId").Value,
            //    _configuration.GetSection("Authentication:Twitter:ClientSecret").Value,
            //    _configuration.GetSection("Authentication:Twitter:AccessToken").Value,
            //    _configuration.GetSection("Authentication:Twitter:AccessTokenSecret").Value);
            //client.RequestUrl = "https://api.twitter.com/2/tweets/search/recent";
            //string auth = client.GetAuthorizationHeader();

            //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(client.RequestUrl);
            //request.Method = "GET";
            //request.Headers.Add("Content-Type", "application/json");
            //request.Headers.Add("Authorization", auth);

            //using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            //{
            //    streamWriter.Write("{\"text\":\"Hello World\"");
            //}
            //var response = (HttpWebResponse)request.GetResponse();
            //var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();



            //HttpResponseMessage? response;
            //using (var client = new HttpClient())
            //{
            //    var url = "https://api.twitter.com/2/tweets/search/recent";
            //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _configuration.GetSection("Authentication:Twitter:Bearer").Value);
            //    response = await client.GetAsync(url);

            //    // Parse JSON response.
            //}

            //foreach (var t in tweets)
            //{
            //    t.Created_at = t.Created_at.Split('+')[0];
            //    t.Text = t.Text.Split("https://")[0];
            //    if (t.Entities.Urls.Count == 0)
            //    {
            //        var urlModel = new UrlModel()
            //        {
            //            Url = ""
            //        };
            //        t.Entities.Urls.Add(urlModel);
            //    }
            //}
            //var a = response;
            var appClient = new TwitterClient(_configuration.GetSection("Authentication:Twitter:ApiKey").Value, 
                _configuration.GetSection("Authentication:Twitter:ApiKeySecret").Value);

            // User client
            var userClient = new TwitterClient(_configuration.GetSection("Authentication:Twitter:ApiKey").Value,
                _configuration.GetSection("Authentication:Twitter:ApiKeySecret").Value,
                _configuration.GetSection("Authentication:Twitter:AccessToken").Value,
                _configuration.GetSection("Authentication:Twitter:AccessTokenSecret").Value);
            // Get the authenticated user
            var authenticatedUser = await userClient.Users.GetAuthenticatedUserAsync();
            var userTimelineIterator = userClient.Timelines.GetUserTimelineIterator("tweetinviapi");


            //var stream = userClient.Streams.CreateFilteredStream();
            //stream.AddTrack("france");

            //stream.MatchingTweetReceived += (sender, eventReceived) =>
            //{
            //    Console.WriteLine(eventReceived.Tweet);
            //};

            //await stream.StartMatchingAnyConditionAsync();

            var searchIterator = userClient.SearchV2.GetSearchTweetsV2Iterator("hello");
            while (!searchIterator.Completed)
            {
                var searchPage = await searchIterator.NextPageAsync();
                var searchResponse = searchPage.Content;
                var tweets = searchResponse.Tweets;
                // ...
            }
            //var a = stream;
            //await AccountActivityCredentialsRetriever.SetUserCredentialsAsync(user.Id, credentials);
            //return $"User {user.Id} registered!";
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public static HttpClient CreateHttpClient(string type, string token)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(type, token);
            return client;
        }
    }
}