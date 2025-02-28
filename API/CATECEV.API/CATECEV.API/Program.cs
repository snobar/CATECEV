using CATECEV.API.Helper.Service;
using CATECEV.API.Helper.IService;
using CATECEV.Data.Configure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureContext();

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddHttpClient<IHttpClientService, HttpClientService>();
builder.Services.AddScoped<IUser, User>();


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
