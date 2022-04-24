using System.Net;
using CatalogService.Application.Exceptions;
using CatalogService.Application.Interfaces.Repositories;
using MediatR;

namespace CatalogService.Application.Features.Queries;

public class DeleteCategoryQuery : IRequest<bool>
{
    public Guid Id { get; set; }

    public class DeleteCategoryHandler : IRequestHandler<DeleteCategoryQuery, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCategoryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteCategoryQuery request,
            CancellationToken cancellationToken)
        {
            try
            {
                var category = await _unitOfWork.Category.GetByIdAsync(request.Id);
                _unitOfWork.Category.Delete(category);
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