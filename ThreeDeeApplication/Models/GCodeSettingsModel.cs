using ThreeDeeApplication.Enums;

namespace ThreeDeeApplication.Models;

public class GCodeSettingsModel
{
    public int Id { get; set; }
    public int SuccesfulPrints { get; set; }
    public int FailedPrints { get; set; }
    public int Downloads { get; set; }
    public double XDimension { get; set; }
    public double YDimension { get; set; }
    public double ZDimension { get; set; }
    public string PrinterBrand { get; set; } = "";
    public string PrinterModel { get; set; } = "";
    public PrintMaterial Material { get; set; }
    public double SuccessPercentage => Math.Round((double)SuccesfulPrints / (FailedPrints + SuccesfulPrints), 2);
    public string XyzDimensions => $"{XDimension}/{YDimension}/{ZDimension}";
}