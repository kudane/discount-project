using DiscountModule;
using Xunit;
using Xunit.Abstractions;

namespace DiscountModule_Test;

public class CouponByAmountCalculatorTests
{
    private class TestCampaign : Campaign
    {
        public TestCampaign(double amount)
        {
            Type = CampaignEnum.CouponByAmount;
            Spec = new CouponByAmountSpec { Amount = amount };
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
        // Counpon 100B, percentOfTotal = 0.25 (25%)
        // discount = 75
        var campaign = new TestCampaign(100);
        var rowItem = new TestRowItem(MockData.Items.First(), 0.25);
        var calculator = new CouponByAmountCalculator(campaign, rowItem);
        var discount = calculator.Execute();
        Assert.True(75 == discount);
    }

    [Fact]
    public void Execute_ZeroAmount_ReturnsOriginalPrice()
    {
        // items 4, per 100B, total 400B
        // Counpon 0B, percentOfTotal = 0.25 (25%)
        // percentOfTotal = 0.25 (25%)
        // discount = 75
        var campaign = new TestCampaign(0);
        var rowItem = new TestRowItem(MockData.Items.First(), 0.25);
        var calculator = new CouponByAmountCalculator(campaign, rowItem);
        var discount = calculator.Execute();
        Assert.True(rowItem.Price == discount);
    }
}