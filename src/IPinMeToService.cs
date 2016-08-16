using System.Net.Http;
using System.Threading.Tasks;
using PinMeTo.Net.Models;

namespace PinMeTo.Net
{
    public interface IPinMeToService
    {
        Task<string> GetToken(string appId, string appSecret);
        Task<HttpResponseMessage> UpdateLocation(ILocationData location, string locationId);
        Task<HttpResponseMessage> AddLocation(ILocationData location);
        Task<ILocationData> GetLocation<T>(string locationId) where T : ILocationData;
    }
}