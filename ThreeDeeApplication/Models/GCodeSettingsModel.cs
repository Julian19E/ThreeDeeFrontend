using System;
using ThreeDeeApplication.Enums;

namespace ThreeDeeApplication.Models;

public class GCodeSettingsModel
{
    public int Id { get; set; }
    public string Author { get; set; }
    public DateTime DateCreated { get; set; }
    public int SuccessfulPrints { get; set; }
    public int FailedPrints { get; set; }
    public float FilamentLengthUsed { get; set; }
    public int PrintingTimeMinutes { get; set; }
    public List<PrintModel> Prints { get; set; }
    public double SuccessPercentage => Math.Round((double)SuccessfulPrints / (FailedPrints + SuccessfulPrints), 2);
}