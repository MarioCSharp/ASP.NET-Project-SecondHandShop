namespace Shop.Controllers.Api
{
    using Microsoft.AspNetCore.Mvc;
    using Shop.Services.Statistics;

    [ApiController]
    [Route("api/statistics")]
    public class StatisticsApiController : ControllerBase
    {
        private readonly IStatisticsService statisticsService;
        public StatisticsApiController(IStatisticsService statisticsService)
        {
            this.statisticsService = statisticsService;
        }
        [HttpGet]
        public StatisticsServiceModel AllStatistics()
        {
            return statisticsService.GetStatistics();
        }
    }
}
