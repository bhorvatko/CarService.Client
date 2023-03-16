namespace CarService.Client.Core.ErrorHandling
{
    public interface IBackgroundErrorHandler
    {
        void HandleBackgroundError(string errorMessage);
    }
}