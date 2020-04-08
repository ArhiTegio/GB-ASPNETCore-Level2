namespace WebStore.Domain.Models
{
    public class Item
    {
        public string Pic { get; set; }
        public string Name { get; set; }
        public string ID { get; set; }
        public string Priсe { get; set; }

        public Item() { }

        public Item(string name, string id, string priсe, string pic)
        {
            Name = name;
            ID = id;
            Priсe = priсe;
            Pic = pic;
        }
    }
}
