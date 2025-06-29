using DiscountModule;
using Xunit;

namespace DiscountModule_Test;

public class SeasonalCalculatorTests
{
    private class TestCampaign : Campaign
    {
        public TestCampaign(double everyPrice, double amount)
        {
            Spec = new SeasonalSpec { EveryPrice = everyPrice, Amount = amount };
        }
    }

    private class TestRowItem : RowItem
    {
        public TestRowItem(Item item, double percentOfTotal)
        {
            Item = item;
            Price = item.Price;
            PercentOfTotalPrice = percentOfTotal;
        }
    }

    [Fact]
    public void Execute_ReturnsDiscountedPrice()
    {
        // items 4, per 100B, total 400B
        // Counpon x=100B Y=10B, percentOfTotal = 0.25 (25%)
        // discount = 90
        var campaign = new TestCampaign(100, 10);
        var rowItem = new TestRowItem(MockData.Items.First(), 0.25);
        var calculator = new SeasonalCalculator(campaign, rowItem, MockData.Items.Sum(a => a.Price));
        var discount = calculator.Execute();
        Assert.True(90 == discount);
    }

    [Fact]
    public void Execute_ZeroEveryPrice_NoDiscount()
    {
        // items 4, per 100B, total 400B
        // Counpon x=100B Y=0B, percentOfTotal = 0.25 (25%)
        // discount = 90
        var campaign = new TestCampaign(100, 0);
        var rowItem = new TestRowItem(MockData.Items.First(), 0.25);
        var calculator = new SeasonalCalculator(campaign, rowItem, MockData.Items.Sum(a => a.Price));
        var discount = calculator.Execute();
        Assert.True(100 == discount);
    }
}