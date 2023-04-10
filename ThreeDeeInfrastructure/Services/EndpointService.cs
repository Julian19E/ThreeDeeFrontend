using ThreeDeeApplication.Models;

namespace ThreeDeeInfrastructure.Services;

public class EndpointService : IEndpointService
{
    private const string FallbackUrl = "localhost/";
    private readonly string _serviceUrl = "http://localhost:3005";
/*
    public EndpointService(IConfigurationRoot configurationRoot)
    {
        _serviceUrl = (string.IsNullOrEmpty(Environment.GetEnvironmentVariable("PCM_SERVICE_URL"))
            ? configurationRoot.GetSection("BackendServices")["Url"]
            : FallbackUrl) ?? FallbackUrl;
    }*/

    public string GetMappedUrl(Type responseModel)
    {
        if (ReferenceEquals(responseModel, typeof(FileModel))) return _serviceUrl + ResourceUrls.ModelsPublic;
        return _serviceUrl;
    }
}