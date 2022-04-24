using System.Net;
using CatalogService.Application.Exceptions;
using CatalogService.Application.Interfaces.Repositories;
using MediatR;

namespace CatalogService.Application.Features.Queries;

public class DeleteProductQuery : IRequest<bool>
{
    public Guid Id { get; set; }

    public class DeleteProductHandler : IRequestHandler<DeleteProductQuery, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteProductHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteProductQuery request,
            CancellationToken cancellationToken)
        {
            try
            {
                var product = await _unitOfWork.Product.GetByIdAsync(request.Id);
                _unitOfWork.Product.Delete(product);
                await _unitOfWork.SaveAsync();
                return true;
            }
            catch (Exception e)
            {
                throw new SCException(HttpStatusCode.BadRequest, "An error has occurred", e);
            }
        }
    }
}