namespace Products2.API.Application.DTO;

public record ProductDTO(
    int? Id,
    string Title,
    string Description,
    double Price,
    int Quantity,
    string Origin);