using GeekShopping.ProductAPI.Model.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var connection = builder.Configuration.GetConnectionString("MySqlConnectionString");
builder.Services.AddDbContext<MySqlContext>(options => options.UseMySql(
    connection,
    ServerVersion.AutoDetect(connection)));

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "GeekShopping.ProductAPI", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "GeekShopping.ProductAPI v1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseAuthorization();

app.MapControllers();

app.Run();
