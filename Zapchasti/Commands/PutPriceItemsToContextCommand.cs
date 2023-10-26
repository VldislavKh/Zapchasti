using MediatR;
using Presentation.Services.Interfaces;

namespace Presentation.Commands
{
    public class PutPriceItemsToContextCommand : IRequest<string>
    {
        public class PutPriceItemsToContextlCommandHandler : IRequestHandler<PutPriceItemsToContextCommand, string>
        {
            private readonly IPutPriceItemsToContext _receiveCsv;

            public PutPriceItemsToContextlCommandHandler(IPutPriceItemsToContext receiveCsv)
            {
                _receiveCsv = receiveCsv;
            }

            public async Task<string> Handle(PutPriceItemsToContextCommand command, CancellationToken cancellationToken)
            {
                return await _receiveCsv.PutPriceItemsToContext("vld1202kh@gmail.com", "pbzvlcijbpzriatr", 1, @"D:\РАБОТА\Практика\Тестовое\Zapchasti\Price\price.csv");
            }
        }
    }
}
