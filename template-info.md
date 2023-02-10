# Erläuterungen zum Template

In Klammern steht ob die jeweiligen Aussagen spezifisch für Blazor sind (dotnet Framework, welches C# als Sprache nutzt) oder allgemein für C# gelten.

## Beginner

### Markup, Code und Styling (Blazor)
Wie in klassischen Frameworks sind die Hauptkomponenten das Markup/Struktur (HTML), das Styling (CSS) und die Funktion/Verhalten (Code).

#### Markup
In Blazor können mit derselben Syntax sich öffnender und schließender Tags alle bekannten html Klassen, sowie eigene Klassen genutzt werden.

```html
<tagname>Inhalt</tagname>
```
oder
```html
<tagname/>
```

#### Styling
Das Styling erfolgt ebenfalls analog entweder global (Pfad zur style.css im root Pfad), über isolierte css Klassen (Dateien oder über das `<style>` Tag), welche genauso heißen wie die Komponente oder Page, oder inline.

**isoliert/global in Datei, z.B. index.razor.css**
```css
.center {
    margin: auto;
    box-shadow: rgba(0, 0, 0, 0.16) 0 1px 4px;
    border-radius: 4px;
}
```

**isoliert im Markup**
```html
<style>    
    .center {
        margin: auto;
        box-shadow: rgba(0, 0, 0, 0.16) 0 1px 4px;
        border-radius: 4px;
    }
</style>
```

**inline**
```html
<MudText Style="padding-top: 50px;">Files</MudText>
```

Isolierte CSS Identifier (Ids, Klassen) treten nicht mit Identifiern gleichen Namens in Konflikt, sofern diese in anderen isolierten Bereichen liegen. Dadurch kann man sehr modular arbeiten.

#### Code
C# Code kann innerhalb des Markups mit denselben syntaktischen Regeln wie auch sonst ausgeführt werden. Allerdings muss darauf mit einem `@` hingewiesen werden, sonst wird der Code einfach als Text gerendert. Auch aufgerufene Properties oder Methoden werden so annotiert:

```html
<div>
    @foreach (var file in _filteredFiles)
    {
        <p>Filename: @file.Name</p>
    }
</div>
```

Die UI-Logik der Komponente (Methoden etc.) befindet sich entweder in derselben .razor Datei oder in einer seperaten Datei, welche nach dem Schema Komponente.razor.cs benannt ist und als `partial class` deklariert werden muss. Wenn sich der Code in derselben Datei befindet muss er mit `@code{...}` annotiert werden:

```html
<PageTitle>3D File Management System</PageTitle>
<MudThemeProvider 
    IsDarkMode="@ThemeProviderService.IsDarkMode"
    IsDarkModeChanged="async x => await SwitchTheme(x)" 
    Theme="_theme"/>
<MudDialogProvider/>
<MudSnackbarProvider/>
<MudLayout>
    <MudAppBar Elevation="1">
        <MudText
            Typo="Typo.h5"
            Class="ml-3">
            Logo
        </MudText>
        <div class="d-flex justify-center flex-grow-1 gap-4">
            <DropDownMenu 
                Label="Library" 
                MenuItems="@TopMenuViewModel.LibraryItems"/>
            <DropDownMenu 
                Label="Files" 
                MenuItems="@TopMenuViewModel.FilesItems"/>
        </div>
        <MudSwitch 
            @bind-Checked="@ThemeProviderService.IsDarkMode"
            Color="Color.Primary" T="bool" 
            Label="@(ThemeProviderService.IsDarkMode ? "DARK" : "LIGHT")"/>
        <MudIconButton 
            Icon="@Icons.Material.Filled.ManageAccounts" 
            Color="Color.Inherit" 
            Edge="Edge.End" />
    </MudAppBar>
    <MudMainContent>
        @Body
    </MudMainContent>
</MudLayout>
```
```csharp
@code
{
    [Inject]
    private IThemeProviderService ThemeProviderService { get; set; }
    
    [Inject] 
    private ProtectedLocalStorage BrowserSettings { get; set; }
    
    private readonly MudTheme _theme = new();
    private const string SaveSettingsKey = "threedeedarkmodesetting";
    
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            var result = await BrowserSettings.GetAsync<bool>(SaveSettingsKey);
            if (result.Success)
            {
                ThemeProviderService.IsDarkMode = result.Value;
            }
            StateHasChanged();
        }
    }

    private async Task SwitchTheme(bool isDarkTheme)
    {
        ThemeProviderService.IsDarkMode = isDarkTheme;
        await BrowserSettings.SetAsync(SaveSettingsKey, isDarkTheme);
    }
}
```

### Komponenten und Pages (Blazor)
Es gibt UI bezogen Komponenten und Pages. Beide werden in einzelnen Dateien gespeichert. Sie sehen fast gleich aus, bis auf den Identifier in der ersten Zeile bei der Page.  
Komponenten können (und sollen) **mehrfach** verwendet werden und können von anderen Komponenten oder Pages instantiiert werden.  
Pages mit dem jeweils eindeutigen Identifier nur **einmal**, da über den Identifier der Pfad definiert wird. Zudem können sie nicht von anderen Pages oder Komponenten instantiiert werden. Hier ruft die Page die Komponente SearchBar auf.

**Komponente (SearchBar.razor):**
```html
<MudAutocomplete
        T="string"
        Immediate="true"
        Label="Suche in verfügbaren Modellen"
        @bind-Value="_selectedValue"
        AdornmentIcon="@Icons.Material.Filled.Search"
        AdornmentColor="Color.Primary"
        SearchFunc="@Search"/>
```

**Page (Index.razor):**
```html
@page "/"
<MudContainer MaxWidth="MaxWidth.Large">
    <MudText Style="padding-top: 50px;" Typo="Typo.h2">Files</MudText>
    <SearchBar 
        FilteredFilesHaveChanged="async (v) => await OnFilteredValueChanged(v)"/>
    <MudGrid Style="margin-top: 25px;">
        ...
    </MudGrid>
</MudContainer>
```

### Parameter (Blazor)
Komponenten oder Seiten, welche andere Komponenten instantiieren, können diesen über Parameter bestimmte Startwerte übergeben (ähnlich eines Konstruktors). Eine Veränderung des Wertes führt zum Rerendering der Komponente (und ausschließlich den betroffenen Teilen des DOM).  
Im Beispiel der Searchbar sieht das Codebehind in der Datei SearchBar.razor.cs so aus:

```csharp
public partial class SearchBar
{
    [Parameter]
    public EventCallback<List<FileModel>> FilteredFilesHaveChanged { get; set; }
```

Auf der Page Index.razr (s.o.) wird vor der schließenden Klammer `>` des öffnenden Tags `<SearchBar` der Wert des Parameters gesetzt (hier eine Callbackfunktion). Parameter müssen mit dem `[Parameter]` **Decorator** versehen werden, **public** sein und über einen **getter und setter** verfügen.

### Properties (C#)
C# benötigt anders als Java keine Referenzen öffentlicher Properties auf private Felder. Properties werden über die Keywords `get;` oder `set;` (alternativ `init;` oder `private set;`) in Verbindung mit dem `protected, internal oder public` Access Modifier initialisiert. 

```csharp
public List<int> Ages { get; } = new();
public List<int> Numbers { get; private set; } = new();
```

Sie können aber auch (analog zu den Gettern und Settern in Java) zusätzlich weitere Aufrufe enthalten. (z.B. ein ausgelöstes Event wenn der Wert sich ändert).
```csharp
public string UserName 
{ 
    get => _username;
    set
    {
        _username = value;
        OnUsernameChanged.InvokeAsync(_username);
    }
}

private string _username;
```

Valuetypes haben automatisch einen Wert wenn ihnen keiner zugewiesen wird (bool: false, int: 0). Properties mit Referencetype sollte entweder direkt oder über den Konstruktor ein Wert zugewiesen werden, ansonsten können sie als nullable reference types deklariert werden:

```csharp
public int Age { get; } // keine Warnung
public string FirstName { get; } // Warnung
public string SurName { get; } = ""; // keine Warnung
public string? SurName { get; } // keine Warnung, aber Warnung beim Aufrufer
```

Startwerte können in C# alternativ oder zusätzlich zum Konstruktor mit den Properties gesetzt werden. Wenn sie public setter haben, können sie auch nachträglich verändert werden:

```csharp
public class UserModel
{
    public int Age { get; set; }
    public string Name { get; }
    private _id;
    
    pulic UserModel(int id)
    {
        _id = id;
    }    
}

public class UserModelCaller
{
    private int _idIncrement;
    
    public void UseModel()
    {
        var user = new UserModel(_idIncrement)
        {
            Age = 22, // könnte man auch weglassen, die ID jedoch nicht
            Name = "Somone"
        }
        user.Age = 23; //geht
        user.Name = "Someone else"; // geht nicht
    }    
}
```

### Program.cs und Properties/launchsettings.json
Die Program.cs ist bei ASP.NET Web (also auch bei Blazor) der Einstiegspunkt de Programms, also analog zur Main in Konsolenanwendungen oder Java. Erstmal nicht weiter drum kümmern, die braucht ihr erst wenn ihr eigene Services registrieren wollt (s. intermediate) weil sie später injected werden sollen.

```csharp
builder.Services.AddHttpClient();
builder.Services.AddMudServices();
builder.Services.AddScoped<TopMenuViewModel>();
builder.Services.AddScoped<IJsInteropService<ModelRenderer>, JsInteropService<ModelRenderer>>();
builder.Services.AddScoped<IThemeProviderService, ThemeProviderService>();
```

In der launchsettings.jon könnt ihr z.B. den Port ändern unter dem die Anwendung lokal läuft (falls es da nen Konflikt geben sollte).
```json
{
  "profiles": {
    "MinimalFrontend": {
      "commandName": "Project",
      "dotnetRunMessages": true,
      "launchBrowser": true,
      "applicationUrl": "https://localhost:5001;http://localhost:5000",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    }
  }
}
```

### Aufruf JS Methoden
### Localization,
### Standards

## Intermediate
### Events und Event Callbacks
### Dependency Injection
### UnitTests, 
### Navigation
### Browser Storage
### Generische Typen
### One und Two Way Binding
### Docker

## Advanced
### Clean Architecture 
### Lifecycles
### Repository Pattern
### MVVM Pattern,
