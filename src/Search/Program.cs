using BusinessLogic.Products;
using Infrastructure.Interfaces;
using Infrastructure.MongoDB.Data;
using Infrastructure.MongoDBDriver;
using Infrastructure.MongoDBDriver.Products;
using Polly;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// /builder.Services.AddTransient<IProductRepository, ProductRepository>();
builder.Services.AddOptions<MongoDbOption>().Bind(builder.Configuration.GetSection(MongoDbOption.Name)).ValidateDataAnnotations();
builder.Services.AddSingleton<MongoDbService>();
builder.Services.AddTransient<IProductRepository, ProductRepositoryMongoDriver>();
builder.Services.AddTransient<ProductService>();

var app = builder.Build();

app.UseExceptionHandler("/error");
app.UseAuthorization();

app.MapControllers();
app.Lifetime.ApplicationStarted.Register(async () =>
{
    await Policy.Handle<TimeoutException>()
        .WaitAndRetryAsync(5, retryAttempt => TimeSpan.FromSeconds(10))
        .ExecuteAndCaptureAsync(async () => await DbInitializer.InitDb());
});

app.UseCors(o => o
    .AllowAnyHeader()
    .AllowAnyMethod()
    .AllowAnyOrigin());

app.Run();