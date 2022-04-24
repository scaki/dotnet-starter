using System.Net;
using CatalogService.Application.Dtos.Category;
using CatalogService.Application.Exceptions;
using CatalogService.Application.Interfaces.Repositories;
using CatalogService.Domain.Entities;
using Mapster;
using MediatR;

namespace CatalogService.Application.Features.Commands
{
    public class PostCategoryCommand : IRequest<PostCategoryDto>
    {
        public string Name { get; set; }

        public class PostCategoryCommandHandler : IRequestHandler<PostCategoryCommand, PostCategoryDto>
        {
            private readonly IUnitOfWork _unitOfWork;

            public PostCategoryCommandHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public Task<PostCategoryDto> Handle(PostCategoryCommand request,
                CancellationToken cancellationToken)
            {
                try
                {
                    var category = new Category
                    {
                        Name = request.Name
                    };

                    _unitOfWork.Category.InsertAsync(category);
                    _unitOfWork.SaveAsync();

                    return Task.FromResult(category.Adapt<PostCategoryDto>());
                }
                catch (Exception e)
                {
                    throw new SCException(HttpStatusCode.BadRequest, "An error has occurred", e);
                }
            }
        }
    }
}