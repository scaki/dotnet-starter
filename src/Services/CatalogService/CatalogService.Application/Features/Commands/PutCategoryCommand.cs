using System.Net;
using CatalogService.Application.Dtos.Category;
using CatalogService.Application.Exceptions;
using CatalogService.Application.Interfaces.Repositories;
using Mapster;
using MediatR;

namespace CatalogService.Application.Features.Commands
{
    public class PutCategoryCommand : IRequest<PutCategoryDto>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public class PutCategoryCommandHandler : IRequestHandler<PutCategoryCommand, PutCategoryDto>
        {
            private readonly IUnitOfWork _unitOfWork;

            public PutCategoryCommandHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<PutCategoryDto> Handle(PutCategoryCommand request,
                CancellationToken cancellationToken)
            {
                try
                {
                    var category = await _unitOfWork.Category.GetByIdAsync(request.Id);
                    category.Name = request.Name;
                    await _unitOfWork.SaveAsync();
                    return category.Adapt<PutCategoryDto>();
                }
                catch (Exception e)
                {
                    throw new SCException(HttpStatusCode.BadRequest, "An error has occurred", e);
                }
            }
        }
    }
}