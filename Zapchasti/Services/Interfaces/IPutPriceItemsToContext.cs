namespace Presentation.Services.Interfaces
{
    public interface IPutPriceItemsToContext
    {
        public Task<string> PutPriceItemsToContext(string email, string password, int providerId, string path);
    }
}
