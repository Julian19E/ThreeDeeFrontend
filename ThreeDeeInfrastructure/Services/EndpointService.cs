using ThreeDeeApplication.Models;
using ThreeDeeInfrastructure.ResponseModels;

namespace ThreeDeeInfrastructure.Services;

public class EndpointService : IEndpointService
{
    private const string FallbackUrl = "localhost/";
    private readonly string _serviceUrl = "http://localhost:3005"; //TODO aus settings lesen
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
        if (ReferenceEquals(responseModel, typeof(FileModelComplete))) return _serviceUrl + ResourceUrls.Model;
        if (ReferenceEquals(responseModel, typeof(GCodeSettingsModel))) return _serviceUrl + ResourceUrls.GCodeFilesInfo;
        return _serviceUrl;
    }
}