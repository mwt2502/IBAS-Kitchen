using System.Collections.Generic;
using System.Threading.Tasks;

public interface ITableStorageService
{
    Task<IEnumerable<MenuItem>> GetMenuItemsAsync();
    Task AddMenuItemAsync(MenuItem menuItem);
}
