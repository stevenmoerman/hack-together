namespace HackTogether.Services;
public class AuthenticationService
{
    public GraphServiceClient GraphServiceClient(Settings settings)
    {
        var interactiveBrowserCredentialOptions = new InteractiveBrowserCredentialOptions
        {
            ClientId = settings.ClientId,
        };
        // Getting the token credential using clientId.
        var tokenCredential = new InteractiveBrowserCredential(interactiveBrowserCredentialOptions);
        // This creates a client with default handlers.
        var graphClient = new GraphServiceClient(tokenCredential, settings.Scopes);

        return graphClient;
    }
}