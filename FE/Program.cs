using CATECEV.Data.Context;
using Microsoft.EntityFrameworkCore;
using CATECEV.Data.Configure;
using CATECEV.FE.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient();

//var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

//builder.Services.AddDbContext<AppDBContext>(options =>
//    options.UseSqlServer(connectionString));

builder.Services.ConfigureContext();
builder.Services.AddDataProtection();
builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("SMTP"));

var app = builder.Build();

AppManager.ConfigureApp(app);

//using (var scope = app.Services.CreateScope())
//{
//    var dbContext = scope.ServiceProvider.GetRequiredService<AppDBContext>();
//    dbContext.Database.Migrate();
//}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
