using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Persistence;

/* to start from the terminal, cd into API folder,
then type: 'dotnet run' or 'dotnet watch run' to attavh file watcher for 
auto rebuilds and restarts */
namespace API
{
  public class Program
  {
    public static async Task Main(string[] args)
    {
      var host = CreateHostBuilder(args).Build();
      // CreateHostBuilder(args).Build().Run();

      // with using statement, scope will be disposed of after 
      using var scope = host.Services.CreateScope();

      var services = scope.ServiceProvider;

      try
      {
        /* using service locator pattern to get DataContext (class we made from Entity DBContext)
        */
        var context = services.GetRequiredService<DataContext>();
        /* Migrate() creates Db if one does not exist*/
        await context.Database.MigrateAsync();

        await Seed.SeedData(context);
      }
      catch (Exception ex)
      {
        /* use ILogger and specify class or type where logging from */
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occured durring migration");
      }
      /*  starts the program */
      await host.RunAsync();

    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
              webBuilder.UseStartup<Startup>();
            });
  }
}
