namespace SP.Contract.API.Services
{
    public interface IHostingEnvironmentService
    {
        void SetEnvironment(bool isProduction);

        bool GetEnvironment();
    }
}
