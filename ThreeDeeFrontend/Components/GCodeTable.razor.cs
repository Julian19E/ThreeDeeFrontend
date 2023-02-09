using Microsoft.AspNetCore.Components;
using ThreeDeeInfrastructure.Repositories;

namespace ThreeDeeFrontend.Components;

public partial class GCodeTable
{
    [Inject]
    private IGCodeSettingsRepository GCodeSettingsRepository { get; set; }
}