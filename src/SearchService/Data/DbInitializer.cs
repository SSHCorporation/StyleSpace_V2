using System.Text.Json;
using MongoDB.Driver;
using MongoDB.Entities;

namespace SearchService
{
    public static class DbInitializer
    {
        public static async Task InitDB(WebApplication app)
        {
            await DB.InitAsync("SearchDB", MongoClientSettings
                .FromConnectionString(app.Configuration.GetConnectionString("MongoDBConnection")));

            await DB.Index<Item>()
                .Key(a => a.Name, KeyType.Text)
                .Key(a => a.Description, KeyType.Text)
                .Key(a => a.CreatedBy, KeyType.Text)
                .CreateAsync();

            var count = await DB.CountAsync<Item>();
            if (count == 0)
            {
                Console.WriteLine("No data - Seeding database...");
                var itemData = await File.ReadAllTextAsync("Data/products.json");
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var items = JsonSerializer.Deserialize<List<Item>>(itemData, options);
                await DB.SaveAsync(items);
            }
        }

    }
}