using Autofac;
using Autofac.Extensions.DependencyInjection;
using Logistics.Business;
using Logistics.Business.DependencyResolvers.Autofac;
using Logistics.DataAccess;
using Logistics.DataAccess.Abstract;
using Logistics.DataAccess.Concrete;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Reflection;

IConfiguration Configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appSettings.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IProductRepository, ProductRepository>();
builder.Services.AddSingleton<IProductService, ProductService>();
builder.Services.AddSingleton<IOrderRepository, OrderRepository>();
builder.Services.AddSingleton<IOrderService, OrderService>();
builder.Services.AddSingleton<IRabbitMQService, RabbitMQService>();
builder.Services.AddDbContext<LogisticsContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DevConnectionString")));

var logger = new LoggerConfiguration()
  .ReadFrom.Configuration(builder.Configuration)
  .Enrich.FromLogContext()
  .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

var assemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();
var assembly = Assembly.Load("Logistics.Business");
assemblies.Add(assembly);

builder.Services.AddAutoMapper(assemblies.ToArray());
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>(builder =>
    {
        builder.RegisterModule(new AutofacBusinessModule());

    });
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
