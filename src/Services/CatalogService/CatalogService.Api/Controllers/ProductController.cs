using CatalogService.Application.Dtos.Product;
using CatalogService.Application.Features.Commands;
using CatalogService.Application.Features.Queries;
using Microsoft.AspNetCore.Mvc;

namespace CatalogService.Api.Controllers;

public class ProductController : BaseController
{
    [HttpGet]
    public Task<List<GetProductsDto>> GetCategories()
    {
        var request = new GetProductsQuery();
        return Mediator.Send(request);
    }


    [HttpPost]
    public Task<PostProductDto> Post(PostProductCommand request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    public Task<PutProductDto> Put(Guid id, PutProductCommand request)
    {
        request.Id = id;
        return Mediator.Send(request);
    }

    [HttpDelete("{id:guid}")]
    public Task<bool> Put(Guid id)
    {
        var request = new DeleteProductQuery() {Id = id};
        return Mediator.Send(request);
    }
}