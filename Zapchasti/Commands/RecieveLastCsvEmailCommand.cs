using MediatR;
using Presentation.Services.Interfaces;

namespace Presentation.Commands
{
    public class RecieveLastCsvEmailCommand : IRequest<string>
    {
        public class RecieveLastCsvEmailCommandHandler : IRequestHandler<RecieveLastCsvEmailCommand, string>
        {
            private readonly IRecieveLastCsvEmail _receiveCsv;

            public RecieveLastCsvEmailCommandHandler(IRecieveLastCsvEmail receiveCsv)
            {
                _receiveCsv = receiveCsv;
            }

            public async Task<string> Handle(RecieveLastCsvEmailCommand command, CancellationToken cancellationToken)
            {
                return _receiveCsv.RecieveLastCsvEmail("vld1202kh@gmail.com", "pbzvlcijbpzriatr");
            }
        }
    }
}
