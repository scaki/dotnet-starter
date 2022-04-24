using System.Net;
using CatalogService.Application.Dtos.Category;
using CatalogService.Application.Exceptions;
using CatalogService.Application.Interfaces.Repositories;
using Mapster;
using MediatR;

namespace CatalogService.Application.Features.Queries;

public class GetCategoriesQuery : IRequest<List<GetCategoriesDto>>
{
    public class GetCategoriesHandler : IRequestHandler<GetCategoriesQuery, List<GetCategoriesDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetCategoriesHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<GetCategoriesDto>> Handle(GetCategoriesQuery request,
            CancellationToken cancellationToken)
        {
            try
            {
                var categories = await _unitOfWork.Category.GetAsyncAsNoTracking(cancellationToken);
                return categories.Adapt<List<GetCategoriesDto>>();
            }
            catch (Exception e)
            {
                throw new SCException(HttpStatusCode.BadRequest, "An error has occurred", e);
            }
        }
    }
}