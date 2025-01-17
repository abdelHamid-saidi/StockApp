

namespace StockApp.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }

        public List<SousCategory> SousCategories { get; set; } = new List<SousCategory>();

        public Category(int categoryId, string name)
        {
            CategoryId = categoryId;
            Name = name;
        }
    }

}
