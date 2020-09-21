using System.Threading.Tasks;
using TVAEnergyData.EIAClient.Models;

namespace TVAEnergyData.EIAClient
{
    public interface IEIARestClient
    {
        Task<EIAResponse> GetSeries(string seriesId, string start = null, string end = null);
    }
}