using InventoryManagement.Core;
using InventoryManagement.Service.Dependency;
using InventoryManagement.Sql.DbDependencies;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

var WebAppCorsPolicy = "WebAppCorsPolicy";

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddAutoMapper(typeof(MapperProfile));
builder.Services.AddDbContextDependencies(builder.Configuration);
builder.Services.AddServiceDependency(builder.Configuration);
builder.Services.AddRepositoryDependency();

var origins = builder.Configuration.GetSection("Domain").Get<Domain>();
if (origins.Client2.Any()) { origins?.Client1?.AddRange(origins.Client2); }

builder.Services.AddCors(options =>
{
    options.AddPolicy(WebAppCorsPolicy,
        builder => builder.WithOrigins(origins?.Client1?.ToArray())
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials());
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(WebAppCorsPolicy);
app.UseAuthorization();
app.UseStaticFiles();


app.MapControllers();

app.Run();
