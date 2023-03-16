// Load appsettings.
var appsettings = Settings.LoadAppSettings();

try
{
    var graphServiceClient = new AuthenticationService().GraphServiceClient(appsettings);
    var lastMonthCalendarEvents = await GraphService.CalendarAsync(graphServiceClient);

    if (lastMonthCalendarEvents.Value != null)
    {
        List<Event> result = new List<Event>();
        result = lastMonthCalendarEvents.Value;
        
        Console.WriteLine($"Calendar items: {result.Count}");
        // Send mail.
        string numberOfEvents = result.Count.ToString();
        await GraphService.SendMail(graphServiceClient, numberOfEvents, appsettings);
    }
    else
    {
        Console.WriteLine("No Events!");
    }
}
catch (System.Exception e)
{
    Console.WriteLine(e.Message);
}
