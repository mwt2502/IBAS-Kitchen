using Azure;
using Azure.Data.Tables;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IBAS_Kitchen.Pages
{
    public class IndexModel : PageModel
    {
        private readonly string connectionString = "DefaultEndpointsProtocol=https;AccountName=ibaskitchen;AccountKey=r2iqjy5ldQubtmEq2J1mt3YC1ajqxntzxp0pexMmKaxMJ2qeUPxAZkkp+vEDMCBNqzgi3XUndEQx+ASt1Sl0zQ==;EndpointSuffix=core.windows.net";
        private readonly string tableName = "IBASKitchenWeek"; 
        private TableClient tableClient;
        public List<MenuItem> MenuItems { get; set; }

        public IndexModel()
        {
            tableClient = new TableClient(connectionString, tableName);
        }

        public async Task OnGetAsync()
        {
            MenuItems = new List<MenuItem>();

            // Fetch data from Azure Table Storage
            await foreach (var entity in tableClient.QueryAsync<TableEntity>())
            {
                // Convert TableEntity to MenuItem
                var menuItem = new MenuItem
                {
                    PartitionKey = entity.PartitionKey,
                    RowKey = entity.RowKey,
                    DishName = entity.GetString("DishName"),
                    Price = entity.GetDouble("Price") ?? 0,
                    Day = entity.GetString("Day")
                };
                MenuItems.Add(menuItem);
            }
        }
    }

    public class MenuItem
    {
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public string DishName { get; set; }
        public double Price { get; set; }
        public string Day { get; set; }
    }
}
