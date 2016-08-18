using System.Collections.Generic;

namespace PinMeTo.Net
{
    public class LocationData : ILocationData
    {
        public string Name { get; set; }
        public string StoreId { get; set; }
        public Contact Contact { get; set; }
        public Address Address { get; set; }
        public bool PermanentlyClosed { get; set; }
        public bool IsAlwaysOpen { get; set; }
        public string LocationDescriptor { get; set; }
        public Location Location { get; set; }
        public OpenHours OpenHours { get; set; }
        public CustomData CustomData { get; set; }
    }

    public class CustomData
    {
        public ShoppingCenter ShoppingCenter { get; set; }
        public string[] Departments { get; set; }
    }

    public class ShoppingCenter
    {
        public string Name { get; set; }
        public string HomePage { get; set; }
    }

    public class Contact
    {
        public string Phone { get; set; }
        public string Homepage { get; set; }
        public string Email { get; set; }
    }

    public class Address
    {
        public string Street { get; set; }
        public string Zip { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }

    public class Location
    {
        public double Lat { get; set; }
        public double Lon { get; set; }
    }

    public class OpeningHourSpan
    {
        public List<OpeningHourDetails> Span { get; set; }
    }

    public class OpeningHourDetails
    {
        public string Open { get; set; }
        public string Close { get; set; }
    }

    public class OpenHours
    {
        public OpeningHourSpan Mon { get; set; }
        public OpeningHourSpan Tue { get; set; }
        public OpeningHourSpan Wed { get; set; }
        public OpeningHourSpan Thu { get; set; }
        public OpeningHourSpan Fri { get; set; }
        public OpeningHourSpan Sat { get; set; }
        public OpeningHourSpan Sun { get; set; }
    }
}