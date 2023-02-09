using ThreeDeeFrontend.Enums;
using ThreeDeeFrontend.Models;

namespace ThreeDeeFrontend.Repositories;

public class GCodeSettingsRepository : IGCodeSettingsRepository
{
    public List<GCodeSettingsModel> MockData { get; } = new()
    {
        new()
        {
            Id = 1,
            SuccesfulPrints = 10,
            FailedPrints = 2,
            Downloads = 50,
            XDimension = 20.0,
            YDimension = 15.0,
            ZDimension = 10.0,
            PrinterBrand = "MakerBot",
            PrinterModel = "Replicator 2",
            Material = PrintMaterial.ABS
        },
        new ()
        {
            Id = 2,
            SuccesfulPrints = 5,
            FailedPrints = 1,
            Downloads = 20,
            XDimension = 25.0,
            YDimension = 20.0,
            ZDimension = 15.0,
            PrinterBrand = "Ultimaker",
            PrinterModel = "Ultimaker 2+",
            Material = PrintMaterial.PLA
        },
        new()
        {
            Id = 3,
            SuccesfulPrints = 8,
            FailedPrints = 0,
            Downloads = 30,
            XDimension = 30.0,
            YDimension = 25.0,
            ZDimension = 20.0,
            PrinterBrand = "Creality",
            PrinterModel = "Ender 3",
            Material = PrintMaterial.PETG
        },
         new GCodeSettingsModel
        {
            Id = 4,
            SuccesfulPrints = 12,
            FailedPrints = 3,
            Downloads = 60,
            XDimension = 22.0,
            YDimension = 18.0,
            ZDimension = 12.0,
            PrinterBrand = "FlashForge",
            PrinterModel = "Adventurer 3",
            Material = PrintMaterial.ABS
        },
        new GCodeSettingsModel
        {
            Id = 5,
            SuccesfulPrints = 7,
            FailedPrints = 2,
            Downloads = 25,
            XDimension = 27.0,
            YDimension = 22.0,
            ZDimension = 17.0,
            PrinterBrand = "Prusa",
            PrinterModel = "i3 MK3S",
            Material = PrintMaterial.PLA
        },
        new GCodeSettingsModel
        {
            Id = 6,
            SuccesfulPrints = 9,
            FailedPrints = 1,
            Downloads = 35,
            XDimension = 32.0,
            YDimension = 27.0,
            ZDimension = 22.0,
            PrinterBrand = "Creality",
            PrinterModel = "CR-10",
            Material = PrintMaterial.PETG
        },
        new GCodeSettingsModel
        {
            Id = 7,
            SuccesfulPrints = 11,
            FailedPrints = 2,
            Downloads = 55,
            XDimension = 24.0,
            YDimension = 20.0,
            ZDimension = 14.0,
            PrinterBrand = "LulzBot",
            PrinterModel = "TAZ 6",
            Material = PrintMaterial.ABS
        },
        new GCodeSettingsModel
        {
            Id = 8,
            SuccesfulPrints = 6,
            FailedPrints = 1,
            Downloads = 22,
            XDimension = 26.0,
            YDimension = 21.0,
            ZDimension = 16.0,
            PrinterBrand = "Ultimaker",
            PrinterModel = "Ultimaker 3",
            Material = PrintMaterial.PLA
        },
        new GCodeSettingsModel
        {
            Id = 9,
            SuccesfulPrints = 10,
            FailedPrints = 2,
            Downloads = 40,
            XDimension = 31.0,
            YDimension = 26.0,
            ZDimension = 21.0,
            PrinterBrand = "Creality",
            PrinterModel = "Ender 5 Pro",
            Material = PrintMaterial.PETG
        }
    };
}