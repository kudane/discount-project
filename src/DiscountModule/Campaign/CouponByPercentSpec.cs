namespace DiscountModule;

public class CouponByPercentSpec : ICampaignSpec
{
    public double Percent { get; set; }

    public void ValidateThrowIfError()
    {
        if (Percent < 0.0)
        {
            throw new ArgumentException("Amount must be zero for CouponByAmountSpec.");
        }

        if (Percent > 100.0)
        {
            throw new ArgumentException("Percent cannot be greater than 100.");
        }
    }
}