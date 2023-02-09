using ThreeDeeApplication.Enums;

namespace ThreeDeeApplication.Models;

public class FileHistoryModel
{
    public DateTime ChangedOn { get; set; }
    public DateTime ByAuthor { get; set; }
    public FileIncident State { get; set; }
    public string Content { get; set; }
}