using MediatR;
using ProductInventory.Application.Exceptions;
using ProductInventory.Application.Repository;

namespace ProductInventory.Application.CQRS.Commands
{
    public class DeleteProduct
    {
        public class Command : IRequest<Unit>
        {
            public Guid Id { get; set; }
        }

        public class DeleteProductCommandHandler : IRequestHandler<Command, Unit>
        {
            private readonly IUnitOfWork _unitOfWork;

            public DeleteProductCommandHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<Unit> Handle(Command command, CancellationToken cancellationToken)
            {
                var product = await _unitOfWork.Products.GetByIdAsync(command.Id);
                if (product == null)
                    throw new NotFoundException($"Product with ID {command.Id} was not found.");

                await _unitOfWork.Products.DeleteAsync(product);
                await _unitOfWork.SaveChangesAsync();
                return Unit.Value;
            }
        }
    }
}
