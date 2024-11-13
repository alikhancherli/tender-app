using Microsoft.EntityFrameworkCore;
using Tender.App.Infra.Configs;
using Tender.App.Infra.Extensions;
using Tender.App.Infra.Filters;
using Tender.App.Infra.Persistence;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DBConnection");

builder.Services.AddControllers(opt => opt.Filters.Add<ApiResultFilter>());
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.RegisterAllServices();
builder.Services.AddDbContext<AppDbContext>(x => x.UseSqlServer(connectionString));

MapperConfig.RegisterMappings();

var app = builder.Build();

var scope = app.Services.CreateScope();
var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
db.Database.Migrate();


app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

app.Run();
