using Azure.Data.Tables;
using Azure.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

public class TableStorageService : ITableStorageService
{
    private readonly TableClient _tableClient;

    public TableStorageService(string tableName)
    {
        // Opret TableServiceClient ved hjælp af den administrerede identitet
        var serviceClient = new TableServiceClient(new Uri("https://ibaskitchenstorage.table.core.windows.net"), new DefaultAzureCredential());
        _tableClient = serviceClient.GetTableClient(tableName);
    }

    public async Task<IEnumerable<MenuItem>> GetMenuItemsAsync()
    {
        var menuItems = new List<MenuItem>();
        await foreach (var entity in _tableClient.QueryAsync<MenuItem>())
        {
            menuItems.Add(entity);
        }
        return menuItems;
    }

    public async Task AddMenuItemAsync(MenuItem menuItem)
    {
        await _tableClient.AddEntityAsync(menuItem);
    }
}
