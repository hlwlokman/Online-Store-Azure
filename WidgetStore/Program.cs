using Azure.Data.Tables;
using Microsoft.Azure.Cosmos;
using Microsoft.EntityFrameworkCore;
using WidgetStore.Data;
using WidgetStore.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Database setup
// I use InMemory database for testing purpose
// In real production may use Azure SQL Database
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseInMemoryDatabase("WidgetStoreDB"));

// Table Storage setup
// I choose Table Storage for orders because:
// - It can handle many orders at same time
// - Good for peak hour traffic
// - Each user email is partition key for fast search
builder.Services.AddSingleton(new TableServiceClient("UseDevelopmentStorage=true"));

// Cosmos DB setup
// I use Cosmos DB for reviews because:
// - Reviews can be different structure (flexible)
// - Can handle anonymous reviews easily
builder.Services.AddSingleton(sp =>
{
    var cosmosClient = new CosmosClient(
        "https://localhost:8081",
        "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==");

    var database = cosmosClient.CreateDatabaseIfNotExistsAsync("WidgetStoreDB").Result;
    database.Database.CreateContainerIfNotExistsAsync("Reviews", "/productId").Wait();

    return cosmosClient;
});

// Service Layer - Business logic
// for loose coupling
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IReviewService, ReviewService>();

var app = builder.Build();

// Seed database
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    context.Database.EnsureCreated();
}

app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthorization();
app.MapControllers();
app.Run();