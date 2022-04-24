using System.Net;
using CatalogService.Application.Dtos.Product;
using CatalogService.Application.Exceptions;
using CatalogService.Application.Interfaces.Repositories;
using Mapster;
using MediatR;

namespace CatalogService.Application.Features.Queries;

public class GetProductsQuery : IRequest<List<GetProductsDto>>
{
    public class GetProductsHandler : IRequestHandler<GetProductsQuery, List<GetProductsDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetProductsHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<GetProductsDto>> Handle(GetProductsQuery request,
            CancellationToken cancellationToken)
        {
            try
            {
                var products = await _unitOfWork.Product.GetProductsWithCategoryAsNoTracking();
                return products.ToList().Adapt<List<GetProductsDto>>();
            }
            catch (Exception e)
            {
                throw new SCException(HttpStatusCode.BadRequest, "An error has occurred", e);
            }
        }
    }
}