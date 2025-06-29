using DiscountModule;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DiscountModule;

public class CampaignConverter : JsonConverter<Campaign>
{
    public override Campaign Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using var jsonDoc = JsonDocument.ParseValue(ref reader);
        var root = jsonDoc.RootElement;

        var typeElement = root.GetProperty("Type").GetString();
        var specElement = root.GetProperty("Spec");

        ICampaignSpec? spec = typeElement switch
        {
            "CouponByAmount" => JsonSerializer.Deserialize<CouponByAmountSpec>(specElement.GetRawText(), options),
            "CouponByPercent" => JsonSerializer.Deserialize<CouponByPercentSpec>(specElement.GetRawText(), options),
            "OntopByPercentOfCatagory" => JsonSerializer.Deserialize<OntopByPercentOfCatagorySpec>(specElement.GetRawText(), options),
            "OntopByPoint" => JsonSerializer.Deserialize<OntopByPointSpec>(specElement.GetRawText(), options),
            "Seasonal" => JsonSerializer.Deserialize<SeasonalSpec>(specElement.GetRawText(), options),
            _ => null
        };

        if(spec == null)
        {
            throw new NotSupportedException($"Unknown campaign type: {typeElement}");
        }

        return new Campaign
        {
            Type = (CampaignEnum)Enum.Parse(typeof(CampaignEnum), typeElement!, true),
            Spec = spec
        };
    }

    public override void Write(Utf8JsonWriter writer, Campaign value, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }
}
