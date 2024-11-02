namespace StonksAPI
{
    public class Program
    {
        /*
         * Main class for the ASP.NET 8.0 CORE Server
         * Here we are creating our server by adding controllers and services and defining the HTTP workflow.
         * We can also register classes to be automatically supplied into methods using DependencyInjection container
         * This class is mainly for setting things up, all the logic will be kept in Controllers and Services
         */
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            // Add services
            builder.Services.AddControllers();

            //Build an app
            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthorization(); // We will be using authorization later

            app.MapControllers();

            app.Run();
        }
    }
}
