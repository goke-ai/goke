using Goke.Bible.Entities;
using Goke.Web.Shared.Models.Markets;

namespace Goke.Web.UI.Models
{
    public class Market
    {
        public static List<Category> CATEGORIES => [
                new(){Id=1, Name="Subscription", 
                    Taxes=TAXES.Where(w=>w.CategoryId==1).ToList() 
                }
                ];

        public static List<Product> PRODUCTS => [
                new(){ Id=1, Name="1 day", Price=1m, CategoryId=1, Category=CATEGORIES.First(f=>f.Id==1),
                Promotions=PROMOTIONS.Where(w=>w.ProductId==1).ToList(),
                },
                new(){ Id=2, Name="7 days", Price=7m, CategoryId=1, Category=CATEGORIES.First(f=>f.Id==1) ,
                Promotions=PROMOTIONS.Where(w=>w.ProductId==2).ToList(),
                },
                new(){ Id=3, Name="28 days", Price=28m, CategoryId=1, Category=CATEGORIES.First(f=>f.Id==1),
                Promotions=PROMOTIONS.Where(w=>w.ProductId==3).ToList(),
                },
            ];

        public static List<ShopItem> SHOPITEMS => [
                new(){ProductId = 1, Product=PRODUCTS.First(f=>f.Id==1) },
                new(){ProductId = 2,  Product=PRODUCTS.First(f=>f.Id==2)},
                new(){ProductId = 3, Product=PRODUCTS.First(f=>f.Id==3) },
                ];

        public static List<Promotion> PROMOTIONS => [
                    new(){DiscountType=DiscountType.Percentage, Discount=5m, ProductId=2, StartDate=DateTime.UtcNow, EndDate=DateTime.UtcNow.AddDays(30)},
                    new(){DiscountType=DiscountType.Percentage, Discount=10m, ProductId=3, StartDate=DateTime.UtcNow, EndDate=DateTime.UtcNow.AddDays(30)}
                    ];

        public static List<Tax> TAXES => [
            new(){ CategoryId=1, Rate=5},
            new(){ CategoryId=2, Rate=10}
            ];
    }
}
