using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Products2.API.Application.DTO;
using Products2.API.Data;
using Products2.API.Data.Enums;

namespace Products2.API.Controllers;

[Route("api/products")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly CatalogDbContext _dbContext;

    public ProductsController(CatalogDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    // GET: api/products
    /// <summary>
    /// Obtêm os produtos
    /// </summary>
    /// <returns>Coleção de objetos da classe Produto</returns>                
    /// <response code="200">Lista dos produtos</response>        
    /// <response code="400">Falha na requisição</response>         
    /// <response code="404">Nenhum produto foi localizado</response>         
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get()
    {
        var products = await _dbContext.Products
            .AsNoTracking()
            .Where(a => a.Status == EntityStatusEnum.Active)
            .Select(p => new ProductDTO(
                p.Id,
                p.Title,
                p.Description,
                p.Price,
                p.Quantity,
                "API 2"
            )).ToListAsync();

        return products.Any() ? Ok(products) : NotFound();
    }
}
