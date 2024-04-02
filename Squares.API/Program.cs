using Squares.API.Filters;
using Squares.Configuration.Options;
using Squares.Database.Memory.Registration;
using Squares.Database.Redis.Registration;
using Squares.Services.Registration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(config =>
{
    config.Filters.Add<CustomExceptionFilterAttribute>();
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSquareIdentifierService();

var dbType = builder.Configuration["AppConfig:DbType"] ?? "RedisDatabase";
builder.Services.Configure<DatabaseOptions>(builder.Configuration.GetSection(dbType));
switch (dbType)
{
    case "RedisDatabase":
        builder.Services.AddRedisRepository();
        break;
    case "MemoryStorage":
        builder.Services.AddMemoryRepository();
        break;
}

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