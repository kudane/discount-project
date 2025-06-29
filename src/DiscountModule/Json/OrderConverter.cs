using DiscountModule;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DiscountModule;

public class OrderConverter : JsonConverter<Order>
{
    public override Order? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using var jsonDoc = JsonDocument.ParseValue(ref reader);
        var root = jsonDoc.RootElement;

        var noElement = root.GetProperty("No").GetInt32();
        var campaignsElement = root.GetProperty("Campaigns");

        return new Order()
        {
            No = noElement,
            Campaigns = campaignsElement.EnumerateArray().Select(c => (CampaignEnum)Enum.Parse(typeof(CampaignEnum), c.GetString()!, true)).ToList()
        };
    }

    public override void Write(Utf8JsonWriter writer, Order value, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }
}
