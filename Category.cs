public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = String.Empty;

    public List<Product> products { get; set; } = new();
}