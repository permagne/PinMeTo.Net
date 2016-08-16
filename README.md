# PinMeTo.Net
A .net client framework for PinMeTo

Add locations:
```c#
ILocationData locationData = new LocationData()
{
   StoreId = "12345",
   Name = "Ye old mechanical keyboard store",              
   // ... And a lot more properties
};
var addReponse = _pinMeToService.AddLocation(locationData);
```

Update locations:

```c#
var updateReponse = _pinMeToService.UpdateLocation(locationData);

```

Retrieve locations:

```c#
ILocationData location = _pinMeToService.GetLocation<LocationData>("1234");

```

###Configuration:###

``` 
<appSettings>
    <add key="PinMeToAppSecret" value="" />  
    <add key="PinMeToAppId" value="" />  
    <add key="PinMeToAppUrl" value="" />  
    <add key="PinMeToApiUrl" value="https://api.pinmeto.com/" />  
</appSettings>

``` 