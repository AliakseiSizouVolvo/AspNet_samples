using Hangfire;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetAcademy.Services.Abstractions;

namespace NetAcademy.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleAggregationController : ControllerBase
    {
        private readonly IArticleService _articleService;
        private readonly IBackgroundJobClient _backgroundJobClient;
        
        public ArticleAggregationController(IArticleService articleService, IBackgroundJobClient backgroundJobClient)
        {
            _articleService = articleService;
            _backgroundJobClient = backgroundJobClient;
        }

        [HttpGet]
        [Route("Health")]
        public async Task<IActionResult> Health()
        {
            //var fireAndForgetJobId = _backgroundJobClient.Enqueue(
            //    () => Console.WriteLine("Fire-and-Forget Job Executed")); //sending emails 

            //var delayedJobId = _backgroundJobClient.Schedule(
            //    () => Console.WriteLine("Scheduled job has been started"),
            //    TimeSpan.FromMinutes(1));

            //var continuationJobId = _backgroundJobClient.ContinueJobWith(
            //    delayedJobId,
            //    () => Console.WriteLine("Scheduled job has been finished. New job started after that"), 
            //    JobContinuationOptions.OnAnyFinishedState);
            
            var rssLink = @"https://www.pcgamesn.com/mainrss.xml";

            RecurringJob.AddOrUpdate($"Article Aggregation {Guid.NewGuid():D}",
                () => _articleService.AggregateFromSourceAsync(rssLink, new CancellationToken()),
                "* 0-1,6-23 * 1-10 1-5");//0:20, 1:20.... 23:20

            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [Route("InitAggregationProcess")]
        [HttpPost]
        public async Task<IActionResult> Aggregate()
        {
            var rssLink = @"https://www.pcgamesn.com/mainrss.xml";

            RecurringJob.AddOrUpdate("Article Aggregation", 
                ()=> _articleService.AggregateFromSourceAsync(rssLink, new CancellationToken()),
                Cron.Hourly(20));//0:20, 1:20.... 23:20

            return NoContent();
        }
    }
}
