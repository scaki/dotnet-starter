using System.Net;
using CatalogService.Application.Dtos.Product;
using CatalogService.Application.Exceptions;
using CatalogService.Application.Interfaces.Repositories;
using CatalogService.Domain.Entities;
using Mapster;
using MediatR;

namespace CatalogService.Application.Features.Commands
{
    public class PostProductCommand : IRequest<PostProductDto>
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public string[] Images { get; set; }
        public string Code { get; set; }
        public int Stock { get; set; }
        public Guid CategoryId { get; set; }

        public class PostProductCommandHandler : IRequestHandler<PostProductCommand, PostProductDto>
        {
            private readonly IUnitOfWork _unitOfWork;

            public PostProductCommandHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<PostProductDto> Handle(PostProductCommand request,
                CancellationToken cancellationToken)
            {
                try
                {
                    var category = await _unitOfWork.Category.GetByIdAsync(request.CategoryId);
                    var product = new Product
                    {
                        Name = request.Name,
                        Price = request.Price,
                        Images = request.Images,
                        Code = request.Code,
                        Stock = request.Stock,
                        Category = category
                    };
                    _unitOfWork.Product.InsertAsync(product);
                    await _unitOfWork.SaveAsync();
                    return product.Adapt<PostProductDto>();
                }
                catch (Exception e)
                {
                    throw new SCException(HttpStatusCode.BadRequest, "An error has occurred", e);
                }
            }
        }
    }
}