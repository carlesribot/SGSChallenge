using BusinessLogic.Products;
using Infrastructure.MongoDB.Data;
using Infrastructure.MongoDB.Products;
using Polly;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddTransient<IProductRepository, ProductRepository>();
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