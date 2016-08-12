namespace PinMeTo.Net
{
    public interface ILocationData
    {
        string Name { get; set; }
        string StoreId { get; set; }
        Contact Contact { get; set; }
        Address Address { get; set; }
        bool PermanentlyClosed { get; set; }
        bool IsAlwaysOpen { get; set; }
        string LocationDescriptor { get; set; }
        Location Location { get; set; }
        OpenHours OpenHours { get; set; }
    }
}