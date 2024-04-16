using Microsoft.EntityFrameworkCore;
using Task_Manager_Beta;
using Task_Manager_Beta.Data;

var builder = WebApplication.CreateBuilder(args);


//----------------------------------------------------------------------------------------------------------------------------------//

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = "TaskManager";
})
.AddCookie("TaskManager", options =>
{
    options.Cookie.Name = "TaskManager";
    options.LoginPath = "/User/Login";
})
.AddGoogle(options =>
{
    options.ClientId = "589004371769-94p5ornaaojva18pcf6hhi5b4kb83oba.apps.googleusercontent.com";
    options.ClientSecret = "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";
});

//----------------------------------------------------------------------------------------------------------------------------------//

builder.Services.AddDbContext<TaskManagerContext>
(options => options.UseSqlServer
(builder.Configuration.GetConnectionString("TaskManagerConnection")));

//----------------------------------------------------------------------------------------------------------------------------------//

builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("SmtpSettings"));

builder.Services.AddScoped<EmailService>();

//----------------------------------------------------------------------------------------------------------------------------------//

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

























//----------------------------------------------------------------------------------------------------------------------------------//

builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

//------------------------------//

app.UseAuthentication();

//------------------------------//

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=LandingPage}/{id?}");

});

app.Run();

//----------------------------------------------------------------------------------------------------------------------------------//
