namespace Goke.Web.UI.Models
{
    public class OrderLineItem
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; } = 1;
        public decimal Price { get; set; }
    }

    public class Customer
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
    }

    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }

    public class ProductPrice
    {
        public int Id { get; set; }
        public Product? Product { get; set; }
        public decimal Price { get; set; }
    }


    public class OrderItem
    {
        public Product? Product { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }

    public class Order
    {
        public int Id { get; set; }
        public Customer? Customer { get; set; }

        public List<OrderItem>? Items { get; set; }
        public ProductPrice? ProductPrice { get; set; }
    }
}
