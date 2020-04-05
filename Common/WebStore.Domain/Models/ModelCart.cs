namespace WebStore.Domain.Models
{
    public class ModelCart
    {
        public Item[] ItemInCart { get; set; }
        public string[] SingleField { get; set; }
        public string[] CityOrRegion { get; set; }

        public ModelCart() { }

        public ModelCart(Item[] itemInCart, string[] singleField, string[] cityOrRegion)
        {
            ItemInCart = itemInCart;
            SingleField = singleField;
            CityOrRegion = cityOrRegion;
        }
    }
}
