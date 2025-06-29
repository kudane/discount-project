using DiscountModule;
using Xunit;


namespace DiscountModule_Test;

public class CouponByPercentCalculatorTests
{
    private class TestCampaign : Campaign
    {
        public TestCampaign(double percent)
        {
            Type = CampaignEnum.CouponByAmount;
            Spec = new CouponByPercentSpec { Percent = percent };
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
        var calculator = new CouponByPercentCalculator(campaign, rowItem, MockData.Items.Sum(a => a.Price));
        var discount = calculator.Execute();
        Assert.True(90 == discount);
    }

    [Fact]
    public void Execute_ZeroPercent_ReturnsOriginalPrice()
    {
        // items 4, per 100B, total 400B
        // Counpon 0%, percentOfTotal = 0.25 (25%)
        // discount = 100
        var campaign = new TestCampaign(0);
        var rowItem = new TestRowItem(MockData.Items.First(), 0.25);
        var calculator = new CouponByPercentCalculator(campaign, rowItem, MockData.Items.Sum(a => a.Price));
        var discount = calculator.Execute();
        Assert.True(100 == discount);
    }
}