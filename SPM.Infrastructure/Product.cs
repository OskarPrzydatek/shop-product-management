namespace SPM.Infrastructure;

public class Product : BaseEntity
{
    public string? Name { get; set; }
    public float Price { get; set; }
    public int CopiesSold { get; set; }
}