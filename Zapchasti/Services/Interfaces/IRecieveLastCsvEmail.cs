namespace Presentation.Services.Interfaces
{
    public interface IRecieveLastCsvEmail
    {
        public void RecieveLastCsvEmail(string email, string password, int providerId, string path);
    }
}
