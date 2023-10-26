using Domain;
using MediatR;
using Presentation.Services.Interfaces;

namespace Presentation.Commands
{
    public class ClearDataBaseCommand : IRequest<Unit>
    {
        public class ClearDataBaseCommandHandler : IRequestHandler<ClearDataBaseCommand, Unit>
        {
            private readonly IDbService _service;

            public ClearDataBaseCommandHandler(IDbService service)
            { 
                _service = service;
            }

            public async Task<Unit> Handle(ClearDataBaseCommand command, CancellationToken cancellationToken)
            {
                await _service.Clear();
                return Unit.Value;
            }
        }
    }
}
