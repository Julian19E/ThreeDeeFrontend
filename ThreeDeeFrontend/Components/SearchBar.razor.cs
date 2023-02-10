using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using ThreeDeeApplication.Models;
using ThreeDeeInfrastructure.Repositories;

namespace ThreeDeeFrontend.Components;

public partial class SearchBar
{
    [Parameter]
    public EventCallback<List<FileModel>> FilteredFilesHaveChanged { get; set; }

    [Inject]
    private IFileRepository FileRepository { get; set; }
    
    private List<FileModel> _showFiltered = new();
    private string _selectedValue;

    protected override void OnParametersSet()
    {
        _showFiltered = FileRepository.MockData;
    }

    private async Task<IEnumerable<string>> Search(string searchValue)
    {
        IEnumerable<string> filtered;
        if (string.IsNullOrEmpty(searchValue))
        {
            _showFiltered = FileRepository.MockData;
            filtered = new List<string>();
        }
        else
        {
            _showFiltered = FileRepository.MockData.Where(x => x.Name
                .Contains(searchValue, StringComparison.InvariantCultureIgnoreCase)).ToList();
            filtered = _showFiltered.Select(x => x.Name);
        }
        await FilteredFilesHaveChanged.InvokeAsync(_showFiltered);
        return filtered;
    }
}