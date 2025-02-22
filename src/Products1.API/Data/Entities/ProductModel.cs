using Products1.API.Domain.DomainObjects;

namespace Products1.API.Data.Entities;

public class ProductModel : BaseEntity, IAggregateRoot
{

    // EF Construtor
    public ProductModel()
    {

    }

    public string Title { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public int Quantity { get; set; }
}
