namespace ThreeDeeFrontend.Models;

public class FileModel
{
    public string Guid { get; set; } = "";
    public string Name { get; set; } = "";
    public string Author { get; set; } = "";
    public DateTime Created { get; set; }
    public Dictionary<int, FileHistoryModel> ChangeHistory { get; set; } = new();
    public long Size { get; set; }
    public int Downloads { get; set; }
    public float AverageRating { get; set; }
    public List<int> GCodeIds { get; set; } = new();
}