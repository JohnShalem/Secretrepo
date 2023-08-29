using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileSystemGlobbing.Internal.PatternContexts;
using Microsoft.OpenApi.Models;
using WhatsAppAPI.Data;
using WhatsAppAPI.IServices;
using WhatsAppAPI.Repository;
using WhatsAppAPI.Services;
using WhatsAppAPI.WhatsAppSettings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddDbContext<RegistrationDbContext>(options => options.UseSqlServer(builder.Configuration.AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["DefaultConnection"]));

var connection = String.Empty;
//if (builder.Environment.IsDevelopment())
//{
//    builder.Configuration.AddEnvironmentVariables().AddJsonFile("appsettings.Development.json");
//    connection = builder.Configuration.GetConnectionString("SqlCon");
//}
//else
//{
//    connection = Environment.GetEnvironmentVariable("SqlCon");
//}
connection = builder.Configuration.GetConnectionString("SqlCon");
builder.Services.AddDbContext<RegistrationDbContext>(options => options.UseSqlServer(connection));
builder.Services.AddTransient<IWhatsAppService,WhatsAppService>();
builder.Services.AddTransient<IWhatsAppIntegrator, WhatsAppIntegrator>();
builder.Services.AddTransient<ICustomerResponses,CustomerResponses>();
builder.Services.AddTransient<IRegistrationService,RegistrationService>();



builder.Services.AddTransient<CustomerRepository>();
builder.Services.AddTransient<FlowRepository>();
builder.Services.AddTransient<CustomerContactRepository>();
builder.Services.AddTransient<FlowDetailsRepository>();
builder.Services.AddTransient<UserPromptsRepository>();
builder.Services.AddTransient<CommunicationRepository>();
builder.Services.AddTransient<UserAnswersRepository>();

builder.Services.Configure<AppSettings>(builder.Configuration.GetSection(nameof(AppSettings)));
//builder.Services.AddSwaggerGen(
//    setupAction =>
//    {
//        setupAction.SwaggerDoc("WhatsAppAPI Documentation", new OpenApiInfo()
//        {
//            Title = "WhatsAppAPI V1",
//            Version = "1",
//        }
//            );
//    }
//    );
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

builder.Services.AddMvcCore().AddDataAnnotationsLocalization();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (true)
{
    app.UseSwagger();
    app.UseSwaggerUI(
        options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "WhatsAppAPI V1");
            options.RoutePrefix = string.Empty;
        });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
