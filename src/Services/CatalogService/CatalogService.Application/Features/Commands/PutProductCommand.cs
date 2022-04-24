using System.Net;
using CatalogService.Application.Dtos.Product;
using CatalogService.Application.Exceptions;
using CatalogService.Application.Interfaces.Repositories;
using CatalogService.Domain.Entities;
using Mapster;
using MediatR;

namespace CatalogService.Application.Features.Commands
{
    public class PutProductCommand : IRequest<PutProductDto>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string[] Images { get; set; }
        public string Code { get; set; }
        public int Stock { get; set; }
        public Guid CategoryId { get; set; }

        public class PutProductCommandHandler : IRequestHandler<PutProductCommand, PutProductDto>
        {
            private readonly IUnitOfWork _unitOfWork;

            public PutProductCommandHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<PutProductDto> Handle(PutProductCommand request,
                CancellationToken cancellationToken)
            {
                try
                {
                    var category = await _unitOfWork.Category.GetByIdAsync(request.CategoryId);
                    var product = await _unitOfWork.Product.GetProductWithCategoryById(request.Id);
                    product.Name = request.Name;
                    product.Price = request.Price;
                    product.Images = request.Images;
                    product.Code = request.Code;
                    product.Stock = request.Stock;
                    product.Category = category;
                    await _unitOfWork.SaveAsync();
                    return product.Adapt<PutProductDto>();
                }
                catch (Exception e)
                {
                    throw new SCException(HttpStatusCode.BadRequest, "An error has occurred", e);
                }
            }
        }
    }
}