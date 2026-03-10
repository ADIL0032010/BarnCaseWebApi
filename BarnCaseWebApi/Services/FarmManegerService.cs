using BarnCaseWebApi.Data;
using Microsoft.EntityFrameworkCore;

namespace BarnCaseWebApi.Services
{
    public class FarmManagerService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        public FarmManagerService(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                    var animals = await context.Animals.ToListAsync();
                    foreach (var a in animals)
                    {
                        if ((DateTime.Now - a.BirthDate).TotalSeconds < a.LifespanInSeconds)
                            a.ProductCount++;
                    }
                    await context.SaveChangesAsync();
                }
                await Task.Delay(2000, stoppingToken); 
            }
        }
    }
}