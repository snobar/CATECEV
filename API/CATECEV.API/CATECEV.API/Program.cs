using CATECEV.API.EntityHelper.IService;
using CATECEV.API.EntityHelper.Service;
using CATECEV.API.Helper.IService;
using CATECEV.API.Helper.Service;
using CATECEV.API.Mapper;
using CATECEV.Data.Configure;


var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureContext();

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddHttpClient<IHttpClientService, HttpClientService>();
builder.Services.AddScoped<IAMPECOUser, AMPECOUser>();
builder.Services.AddScoped<IAMPECOChargePoints, AMPECOChargePoints>();
builder.Services.AddScoped<IAMPECOSessions, AMPECOSessions>();
builder.Services.AddScoped<IAMPECOTaxes, AMPECOTaxes>();
builder.Services.AddScoped<IAMPECOEvses, AMPECOEvses>();
builder.Services.AddScoped<IUser, User>();
builder.Services.AddScoped<ITax, Tax>();
builder.Services.AddScoped<IConnector, Connector>();
builder.Services.AddScoped<IChargePoint, ChargePoint>();
builder.Services.AddScoped<IEvse, Evse>();
builder.Services.AddScoped(typeof(IAMPECOResource<>), typeof(AMPECOResource<>));


builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "My API", Version = "v1" });
});

var app = builder.Build();

AppManager.ConfigureApp(app);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
