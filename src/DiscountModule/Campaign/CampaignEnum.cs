using System.Text.Json.Serialization;

namespace DiscountModule;

public enum CampaignEnum
{
    CouponByAmount = 1,
    CouponByPercent = 2,
    OntopByPercentOfCatagory = 3,
    OntopByPoint = 4,
    Seasonal = 5,
}