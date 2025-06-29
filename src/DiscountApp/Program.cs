using DiscountApp;
using DiscountModule;
using System.Text.Json;

public static class Program
{
    private static readonly JsonSerializerOptions JsonSerializerOptions = new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true,
        Converters = { new CampaignConverter(), new OrderConverter() }
    };

    public static void Main(string[] args)
    {
        var jsonString = File.ReadAllText("data.json");

        var model = JsonSerializer.Deserialize<DataModel>(jsonString, JsonSerializerOptions)!;

        var totalAmount = model.Items.Sum(item => item.Price);

        var calculator = new Calculator(model.Items, model.Campaigns, model.Orders);

        var totalNet = calculator.Execute();

        // Print table header
        Console.WriteLine("{0,-20} {1,-15} {2,10}", "Name", "Category", "Price");
        Console.WriteLine(new string('-', 50));

        // Print each item as a row in the table
        foreach (var item in model.Items)
        {
            Console.WriteLine("{0,-20} {1,-15} {2,10:F2}", item.Name, item.Catagory, item.Price);
        }

        Console.WriteLine($"Total Amount: {totalAmount}");
        Console.WriteLine($"Total Net: {totalNet}");
    }
}
