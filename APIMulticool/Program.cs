using System.Configuration;
using APIMulticool.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddControllers();
        var CnnStrBuilder = new SqlConnectionStringBuilder(builder.Configuration.GetConnectionString("CNNSTR"));
        string cnnStr = CnnStrBuilder.ConnectionString;
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        //builder.Services.AddDbContext<MulticoolDBContext>(options
        //    => options.UseSqlServer(builder.Configuration.GetConnectionString(
        //        "CNNSTR")));
        builder.Services.AddDbContext<MulticoolDBContext>(
                options => options.UseSqlServer(
                    "SERVER=LAPTOP-0OCE7TFC\\SQLEXPRESS;DATABASE=MulticoolDB;INTEGRATED SECURITY=TRUE;User Id=;Password=",
                    providerOptions => providerOptions.EnableRetryOnFailure()));

        //static void ConfigureServices(IServiceCollection services)
        //{
        //    services.AddDbContext<MulticoolDBContext>(
        //        options => options.UseSqlServer(
        //            "SERVER=LAPTOP-0OCE7TFC;DATABASE=MulticoolDB;INTEGRATED SECURITY=TRUE;User Id=;Password=",
        //            providerOptions => providerOptions.EnableRetryOnFailure()));
        //}
        var app = builder.Build();
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseRouting();
        
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}
