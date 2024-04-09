using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Task_Manager_Beta.Data;

var builder = WebApplication.CreateBuilder(args);
//---------------------------------------------------------------------------------------//
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = "TaskManger"; // Sử dụng scheme "TaskMange" làm scheme mặc định
})
.AddCookie("TaskManger", options => // Sử dụng scheme "TaskMange" cho cookie authentication
{
    options.Cookie.Name = "TaskManger";
    options.LoginPath = "/User/Login";
})
.AddGoogle(options =>
{
    options.ClientId = "589004371769-94p5ornaaojva18pcf6hhi5b4kb83oba.apps.googleusercontent.com";
    options.ClientSecret = "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";
});

//---------------------------------------------------------------------------------------//

builder.Services.AddDbContext<TaskManagerContext>
(options => options.UseSqlServer
(builder.Configuration.GetConnectionString("TaskManagerConnection")));

//---------------------------------------------------------------------------------------//


// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

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

app.UseAuthentication();

app.UseAuthorization();


app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "dashboard",
        pattern: "dashboard/{id?}",
        defaults: new { controller = "Projects", action = "DashBoard" }
    );

    endpoints.MapControllerRoute(
        name: "dang-ky",
        pattern: "dang-ky",
        defaults: new { controller = "User", action = "Register" });
    endpoints.MapControllerRoute(
       name: "google-login-callback",
       pattern: "google-login-callback",
       defaults: new { controller = "User", action = "ExternalLoginCallback" });

    endpoints.MapControllerRoute(
    name: "dang-nhap",
    pattern: "dang-nhap",
    defaults: new { controller = "User", action = "Login" });


    endpoints.MapControllerRoute(
    name: "dang-xuat",
    pattern: "dang-xuat",
    defaults: new { controller = "User", action = "Logout" });

    app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=LandingPage}/{id?}");

});

app.Run();

