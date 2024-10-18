using AutoMapper;
using MediatR;
using ProductInventory.Application.DTOs;
using ProductInventory.Application.Exceptions;
using ProductInventory.Application.Repository;

namespace ProductInventory.Application.CQRS.Queries
{
    public class GetProductById
    {
        public class Query : IRequest<ProductDto>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, ProductDto>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;

            public Handler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }

            public async Task<ProductDto> Handle(Query query, CancellationToken cancellationToken)
            {
                var product = await _unitOfWork.Products.GetByIdAsync(query.Id);

                if (product == null)
                    throw new NotFoundException($"Product with ID {query.Id} was not found.");

                return _mapper.Map<ProductDto>(product);
            }
        }
    }
}
