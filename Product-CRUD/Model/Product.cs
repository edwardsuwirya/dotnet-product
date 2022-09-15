namespace ProductCRUD.Model
{
    public class Product
    {
        public string id { get; set; }
        public string productName { get; set; }

        public Product(string id, string productName)
        {
            this.id = id;
            this.productName = productName;
        }
        public override string ToString() => $"{id}-{productName}";
    }
}

