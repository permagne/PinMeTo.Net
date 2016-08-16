using System.Net.Http;
using System.Threading.Tasks;

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