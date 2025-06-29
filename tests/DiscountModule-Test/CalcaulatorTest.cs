using DiscountApp;
using DiscountModule;
using System.Text.Json;
using Xunit;
using Xunit.Abstractions;

namespace DiscountModule_Test;

public class CalcaulatorTest
{
    private readonly DataModel model;


    [Fact]
    public void Execute_ReturnsDiscountedPrice_CouponByAmount()
    {
        var jsonString = File.ReadAllText("Json/CouponByAmount.json");

        var model = JsonSerializer.Deserialize<DataModel>(jsonString, MockData.JsonSerializerOptions)!;

        var totalAmount = model.Items.Sum(item => item.Price);

        var calculator = new Calculator(model.Items, model.Campaigns, model.Orders);

        var totalNet = calculator.Execute();

        Assert.True(300 == totalNet);
    }


    [Fact]
    public void Execute_ReturnsDiscountedPrice_OntopByPoint()
    {
        var jsonString = File.ReadAllText("Json/OntopByPoint.json");

        var model = JsonSerializer.Deserialize<DataModel>(jsonString, MockData.JsonSerializerOptions)!;

        var totalAmount = model.Items.Sum(item => item.Price);

        var calculator = new Calculator(model.Items, model.Campaigns, model.Orders);

        var totalNet = calculator.Execute();

        Assert.True(320 == totalNet);
    }

    [Fact]
    public void Execute_ReturnsDiscountedPrice_Seasonal()
    {
        var jsonString = File.ReadAllText("Json/Seasonal.json");

        var model = JsonSerializer.Deserialize<DataModel>(jsonString, MockData.JsonSerializerOptions)!;

        var totalAmount = model.Items.Sum(item => item.Price);

        var calculator = new Calculator(model.Items, model.Campaigns, model.Orders);

        var totalNet = calculator.Execute();

        Assert.True(360 == totalNet);
    }
}