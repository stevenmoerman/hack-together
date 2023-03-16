namespace HackTogether.Services;
public class GraphService
{
    public static async Task<EventCollectionResponse> CalendarAsync(GraphServiceClient graphServiceClient)
    {
        var result = new EventCollectionResponse();
        var today = DateTime.Today;
        var month = new DateTime(today.Year, today.Month, 1);
        var firstDayOfTheMonth = month.AddMonths(-1);
        var lastDayOfTheMonth = month;

        var calendar = await graphServiceClient.Me.CalendarView.GetAsync(requestConfiguration =>
        {
            requestConfiguration.QueryParameters.StartDateTime = firstDayOfTheMonth.ToString("o");
            requestConfiguration.QueryParameters.EndDateTime = lastDayOfTheMonth.ToString("o");
            requestConfiguration.QueryParameters.Select = new string[] { "id", "subject", "start", "end" };
            requestConfiguration.QueryParameters.Top = 100;
            requestConfiguration.QueryParameters.Orderby = new string[] { "start/dateTime" };
        });
        if (calendar != null)
        {
            result = calendar;
        }
        return result;
    }

    public static async Task SendMail(GraphServiceClient graphServiceClient, string count, Settings settings)
    {
        var requestBody = new Microsoft.Graph.Me.SendMail.SendMailPostRequestBody
        {
            Message = new Message
            {
                Subject = "Last Month Calendar Items",
                Body = new ItemBody
                {
                    ContentType = BodyType.Text,
                    Content = $"{count} calendar items last month!",
                },
                ToRecipients = new List<Recipient>
        {
            new Recipient
            {
                EmailAddress = new EmailAddress
                {
                    Address = settings.To,
                },
            },
        },
            },
        };
        // Send mail.
        await graphServiceClient.Me.SendMail.PostAsync(requestBody);
    }
}