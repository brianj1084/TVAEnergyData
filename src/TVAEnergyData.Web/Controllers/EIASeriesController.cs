using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TVAEnergyData.EIAClient;

namespace TVAEnergyData.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EIASeriesController : ControllerBase
    {
        private readonly IEIARestClient _eiaRestClient;

        public EIASeriesController(IEIARestClient eiaRestClient)
        {
            _eiaRestClient = eiaRestClient;
        }

        [HttpGet]
        [Route("{seriesId}")]
        public async Task<Models.Series> GetSeries(string seriesId, string start = null, string end = null)
        {
            var eiaSeriesResponse = await _eiaRestClient.GetSeries(seriesId, start, end);

            return Models.Series.FromEIAResponse(eiaSeriesResponse);
        }
    }
}
