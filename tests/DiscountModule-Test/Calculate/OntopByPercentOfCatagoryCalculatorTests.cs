using DiscountModule;
using Xunit;

namespace DiscountModule_Test;

public class OntopByPercentOfCatagoryCalculatorTests
{
    private class TestCampaign : Campaign
    {
        public TestCampaign(double percent)
        {
            Spec = new OntopByPercentOfCatagorySpec { Percent = percent, ItemCategory = "Clothing" };
        }
    }

    private class TestRowItem : RowItem
    {
        public TestRowItem(Item item, double percentOfTotalPrice)
        {
            Item = item;
            Price = item.Price;
            PercentOfTotalPrice = percentOfTotalPrice;
        }
    }

    [Fact]
    public void Execute_ReturnsDiscountedPrice()
    {
        // items 4, per 100B, total 400B
        // Counpon 10%, percentOfTotal = 0.25 (25%)
        // discount = 90
        var campaign = new TestCampaign(10);
        var rowItem = new TestRowItem(MockData.Items.First(), 0.25);
        var calculator = new OntopByPercentOfCatagoryCalculator(campaign, rowItem);
        var discount = calculator.Execute();
        Assert.True(90 == discount);
    }

    [Fact]
    public void Execute_ZeroPercent_ReturnsOriginalPrice()
    {
        // items 4, per 100B, total 400B
        // Counpon 10%, percentOfTotal = 0.25 (25%)
        // discount = 90
        var campaign = new TestCampaign(0.0);
        var rowItem = new TestRowItem(MockData.Items.First(), 0.25);
        var calculator = new OntopByPercentOfCatagoryCalculator(campaign, rowItem);
        var discount = calculator.Execute();
        Assert.True(100 == discount);
    }
}