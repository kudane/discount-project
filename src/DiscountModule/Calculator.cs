namespace DiscountModule;

public class Calculator
{
    private readonly IEnumerable<Order> orders;
    private readonly List<RowItem> rowItems = [];

    public Calculator(IEnumerable<Item> items, IEnumerable<Campaign> campaigns, IEnumerable<Order> orders)
    {
        this.orders = orders; ;

        var totalPriceInCart = items.Sum(a => a.Price);

        foreach (var item in items)
        {
            var row = new RowItem()
            {
                Item = item,
                PercentOfTotalPrice = item.Price / totalPriceInCart,                                // สัดส่วนราคาของแต่ละชิ้น
                Price = item.Price,
                Campaigns = campaigns,                                                              // ราคาตั้งต้นของแต่ละชิ้น
            };

            rowItems.Add(row);
        }
    }

    public double Execute()
    {
        foreach (var orderItem in orders)
        {
            var totalPriceInCart = rowItems.Sum(a => a.Price);

            foreach (var rowItem in rowItems)
            {
                if(rowItem.Campaigns == null)
                {
                    continue;
                }

                var campaign = rowItem.Campaigns.FirstOrDefault(a => orderItem.Campaigns.Any(id => id == a.Type));

                if (campaign == null) continue;

                if(campaign.Ignore != null && campaign.Ignore(rowItem.Item, campaign))
                {
                    continue;
                }

                var calculator = CalculateFactory.GetCalculator(campaign ,rowItem, totalPriceInCart);

                if (calculator == null) continue;

                rowItem.Price = calculator.Execute();
            }
        }

        return rowItems.Sum(a => a.Price);
    }
}
