using RediRND.App.Repositories;

namespace RediRND.App.Services
{
    public class ProfitService
    {
        private readonly ContainerRepository _containerRepository;

        public ProfitService(ContainerRepository containerRepository) 
        {
            _containerRepository = containerRepository;
        }

        public async Task RunProfitCalculation(DateTime dateTime, decimal profitMade)
        {
            await Task.CompletedTask;
        }
    }
}
