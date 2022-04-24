using CatalogService.Application.Dtos.Category;
using CatalogService.Application.Features.Commands;
using CatalogService.Application.Features.Queries;
using Microsoft.AspNetCore.Mvc;

namespace CatalogService.Api.Controllers;

public class CategoryController : BaseController
{
    [HttpGet]
    public Task<List<GetCategoriesDto>> GetCategories()
    {
        var request = new GetCategoriesQuery();
        return Mediator.Send(request);
    }

    [HttpPost]
    public Task<PostCategoryDto> Post(PostCategoryCommand request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    public Task<PutCategoryDto> Put(Guid id, PutCategoryCommand request)
    {
        request.Id = id;
        return Mediator.Send(request);
    }

    [HttpDelete("{id:guid}")]
    public Task<bool> Put(Guid id)
    {
        var request = new DeleteCategoryQuery() {Id = id};
        return Mediator.Send(request);
    }
}