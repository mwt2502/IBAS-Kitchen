using Microsoft.AspNetCore.Mvc.RazorPages;

public class IndexModel : PageModel
{
    private readonly ITableStorageService _tableStorageService;

    public IEnumerable<MenuItem> MenuItems { get; set; }

    public IndexModel(ITableStorageService tableStorageService)
    {
        _tableStorageService = tableStorageService;
    }

    public async Task OnGetAsync()
    {
        MenuItems = await _tableStorageService.GetMenuItemsAsync();
    }
}
