using ContactManager.Server.Contexts;
using Microsoft.EntityFrameworkCore;

namespace ContactManager.Server;

public class Program
{
    public static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddDbContext<ContactContext>(options =>
                                                              options.UseSqlServer(builder.Configuration
                                                                     .GetConnectionString("Default")));

        builder.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(corsBuilder =>
            {
                corsBuilder.AllowAnyOrigin()
                           .AllowAnyHeader()
                           .AllowAnyMethod();
            });
        });

        WebApplication app = builder.Build();

        app.UseDefaultFiles();
        app.UseStaticFiles();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
        app.MapFallbackToFile("/index.html");
        app.UseCors();

        app.Run();
    }
}
