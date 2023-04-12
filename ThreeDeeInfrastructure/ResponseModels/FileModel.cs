using System;
using System.Collections.Generic;
using ThreeDeeApplication.Enums;
using ThreeDeeInfrastructure.ResponseModels;

namespace ThreeDeeApplication.Models;

public class FileModel : ResponseBase
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Author { get; set; } = "";
    public DateTime Created { get; set; }
    public long Size { get; set; }
    public float Rating { get; set; } 
    public List<int> GCodeIds { get; set; } = new();
    public Filetype Filetype { get; set; }
}