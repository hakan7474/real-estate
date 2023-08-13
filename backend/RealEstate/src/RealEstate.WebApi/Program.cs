using RealEstate.Application;
using RealEstate.Infrastructure;
using RealEstate.Persistence;
using RealEstate.Persistence.Context;
using RealEstate.WebApi.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors();
builder.Services.AddControllers(opt =>
{
    opt.Filters.Add<ValidationFilter>();
    opt.Filters.Add<ExceptionFilter>();
}).AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
    options.JsonSerializerOptions.IgnoreReadOnlyProperties = true;
    options.JsonSerializerOptions.WriteIndented = true;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddPersistence(builder.Configuration)
    .AddInfrastructure(builder.Configuration)
    .AddApplication(builder.Configuration)
    .AddTransient<ValidationFilter>();

var app = builder.Build();

RealEstateSeed.ApplyDbMigrationsWithDataSeedAsync(app.Services).GetAwaiter().GetResult();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseCors(c =>
{
    c.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin().SetIsOriginAllowedToAllowWildcardSubdomains();
});


app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
