using AutoMapper;
using MediatR;
using ProductInventory.Application.DTOs;
using ProductInventory.Application.Exceptions;
using ProductInventory.Application.Repository;

namespace ProductInventory.Application.CQRS.Commands
{
    public class UpdateProduct
    {
        public class Command : IRequest<ProductDto>
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public decimal Price { get; set; }
            public Guid UserId { get; set; }

            public Command(Guid id, string name, decimal price, Guid userId)
            {
                Id = id;
                Name = name;
                Price = price;
                UserId = userId;
            }
        }

        public class UpdateProductCommandHandler : IRequestHandler<Command, ProductDto>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;

            public UpdateProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }

            public async Task<ProductDto> Handle(Command command, CancellationToken cancellationToken)
            {
                var product = await _unitOfWork.Products.GetByIdAsync(command.Id);
                if (product == null)
                    throw new NotFoundException($"Product with ID {command.Id} was not found.");

                product.Name = command.Name;
                product.Price = command.Price;
                product.ModifiedOn = DateTimeOffset.Now;
                product.ModifiedBy = command.UserId;

                await _unitOfWork.Products.UpdateAsync(product);
                await _unitOfWork.SaveChangesAsync();

                return _mapper.Map<ProductDto>(product);
            }
        }
    }
}
