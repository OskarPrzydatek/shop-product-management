namespace SPM.Infrastructure.Exception;

public class ProductNotFoundException: System.Exception
{
    public ProductNotFoundException(string? message) : base(message)
    {
    }
}