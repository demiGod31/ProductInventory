using AutoMapper;
using MediatR;
using ProductInventory.Application.DTOs;
using ProductInventory.Application.Repository;
using ProductInventory.Domain.Entities;

namespace ProductInventory.Application.CQRS.Commands
{
    public class CreateProduct
    {
        public class Command : IRequest<ProductDto>
        {
            public string Name { get; set; }
            public decimal Price { get; set; }
            public Guid UserId { get; set; }

            public Command(string name, decimal price, Guid userId)
            {
                Name = name;
                Price = price;
                UserId = userId;
            }
        }

        public class Handler : IRequestHandler<Command, ProductDto>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;

            public Handler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }

            public async Task<ProductDto> Handle(Command command, CancellationToken cancellationToken)
            {
                var product = new Product { Name = command.Name, Price = command.Price, CreatedBy = command.UserId, ModifiedBy = command.UserId };
                await _unitOfWork.Products.AddAsync(product);
                await _unitOfWork.SaveChangesAsync();
                return _mapper.Map<ProductDto>(product);
            }
        }
    }
}
