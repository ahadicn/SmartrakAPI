using Microsoft.EntityFrameworkCore;
using SmarTrakWebData.DBEntities;
using SmarTrakWebData.Repositories;
using SmarTrakWebDomain.Repositories;
using SmarTrakWebDomain.Services;
using SmarTrakWebService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Register AzureTestContext in DI
builder.Services.AddDbContext<AzureTestContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


// Register other services
builder.Services.AddHttpClient<IAzureFunctionService, AzureFunctionService>();
builder.Services.AddScoped<IOrganizationRepository, OrganizationRepository>();
builder.Services.AddScoped<IOrganizationService, OrganizationService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddControllers();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "SmartrakAPI V1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
