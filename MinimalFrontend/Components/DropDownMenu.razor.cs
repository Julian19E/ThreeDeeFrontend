using Microsoft.AspNetCore.Components;
using MinimalFrontend.Models;

namespace MinimalFrontend.Components;

public partial class DropDownMenu
{
    [Parameter]
    public List<DropDownMenuItemModel> MenuItems { get; set; } 
    
    [Parameter]
    public string Label { get; set; }
}