﻿using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using ThreeDeeApplication.Models;

namespace ThreeDeeFrontend.Components;

public partial class DropDownMenu
{
    [Parameter]
    public List<DropDownMenuItemModel> MenuItems { get; set; } 
    
    [Parameter]
    public string Label { get; set; }
}