using System.Collections.Generic;
using ThreeDeeApplication.Enums;
using ThreeDeeApplication.Models;

namespace ThreeDeeInfrastructure.Repositories;

public class GCodeSettingsRepository : IGCodeSettingsRepository
{
    public List<GCodeSettingsModel> MockData { get; } = new()
    {
        new()
        {
            Id = 1,
            SuccessfulPrints = 10,
            FailedPrints = 2
        },
        new ()
        {
            Id = 2,
            SuccessfulPrints = 5,
            FailedPrints = 1
        },
        new()
        {
            Id = 3,
            SuccessfulPrints = 8,
            FailedPrints = 0
        },
         new GCodeSettingsModel
        {
            Id = 4,
            SuccessfulPrints = 12,
            FailedPrints = 3
        },
        new GCodeSettingsModel
        {
            Id = 5,
            SuccessfulPrints = 7,
            FailedPrints = 2
        },
        new GCodeSettingsModel
        {
            Id = 6,
            SuccessfulPrints = 9,
            FailedPrints = 1
        },
        new GCodeSettingsModel
        {
            Id = 7,
            SuccessfulPrints = 11,
            FailedPrints = 2
        },
        new GCodeSettingsModel
        {
            Id = 8,
            SuccessfulPrints = 6,
            FailedPrints = 1
        },
        new GCodeSettingsModel
        {
            Id = 9,
            SuccessfulPrints = 10,
            FailedPrints = 2
        }
    };
}