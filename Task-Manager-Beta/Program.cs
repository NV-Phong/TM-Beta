using Microsoft.EntityFrameworkCore;
using Task_Manager_Beta.Data;

var builder = WebApplication.CreateBuilder(args);

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

app.UseAuthorization();


app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "dashboard",
        pattern: "dashboard/{id?}",
        defaults: new { controller = "Projects", action = "DashBoard" }
    );

    app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=LandingPage}/{id?}");

});

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=LandingPage}/{id?}");

app.Run();

