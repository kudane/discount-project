using DiscountModule;
using Xunit;

namespace DiscountModule_Test;

public class OntopByPointCalculatorTests
{
    private class TestCampaign : Campaign
    {
        public TestCampaign(double point)
        {
            Spec = new OntopByPointSpec { Point = point };
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
        // Counpon 100Point, percentOfTotal = 0.25 (25%)
        // discount = 75
        var campaign = new TestCampaign(100);
        var rowItem = new TestRowItem(MockData.Items.First(), 0.25);
        var calculator = new OntopByPointCalculator(campaign, rowItem, MockData.Items.Sum(a => a.Price));
        var discount = calculator.Execute();
        Assert.True(80 == discount);
    }

    [Fact]
    public void Execute_PointExceedsLimit_CappedAt20Percent()
    {
        // items 4, per 100B, total 400B
        // Counpon 0Point, percentOfTotal = 0.25 (25%)
        // discount = 75
        var campaign = new TestCampaign(0);
        var rowItem = new TestRowItem(MockData.Items.First(), 0.25);
        var calculator = new OntopByPointCalculator(campaign, rowItem, MockData.Items.Sum(a => a.Price));
        var discount = calculator.Execute();
        Assert.True(100 == discount);
    }
}