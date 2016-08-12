using System.Threading.Tasks;

namespace PinMeTo.Net
{
    public interface IPinMeToService
    {
        Task<string> GetToken(string appId, string appSecret);
        Task<string> UpdateLocation(ILocationData location, string locationId);
        Task<string> AddLocation(ILocationData location);
        Task<ILocationData> GetLocation<T>(string locationId) where T : ILocationData;
    }
}