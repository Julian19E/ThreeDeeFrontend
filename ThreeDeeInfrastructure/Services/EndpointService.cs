using ThreeDeeApplication.Models;
using ThreeDeeInfrastructure.ResponseModels;

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
        if (ReferenceEquals(responseModel, typeof(FileModelPrivate))) return _serviceUrl + ResourceUrls.ModelsPrivate;
        if (ReferenceEquals(responseModel, typeof(FileModelComplete))) return _serviceUrl + ResourceUrls.Model;
        return _serviceUrl;
    }
}