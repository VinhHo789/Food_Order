using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Lab05.Models;

using Microsoft.AspNetCore.Identity;

/*Các đường link:----------------------------------------------
 default: Trang Login
 /Order/PlaceOrder: Trang đặt đồ ăn cho khách hàng
 /Customer/index: Trang quản lý đơn hàng
/Signup/signup: Đăng ký
*/



var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:7252"
                                              ).AllowAnyHeader()
                                               .AllowAnyMethod();
                      });
});


// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<foodContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("local"));
});

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseCors(MyAllowSpecificOrigins);
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();




// Assuming you're using app.UseEndpoints() in your Configure method
app.UseEndpoints(endpoints =>
{
    // The "login" route
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Login}/{action=Login}/{id?}",
        defaults: new { controller = "Login" });

    // The "PlaceOrder" route
    endpoints.MapControllerRoute(
        name: "PlaceOrder",
        pattern: "{controller=Order}/{action=PlaceOrder}/{id?}");

    // The "PlaceOrder" route
    endpoints.MapControllerRoute(
        name: "CustomerOrder",
        pattern: "{controller=Customer}/{action=CustomerOrder}/{id?}");

    // Add this to your startup.cs or wherever you configure routes
    endpoints.MapControllerRoute(
        name: "DeleteOrder",
        pattern: "Customer/DeleteOrder/{maHD}",
        defaults: new { controller = "Customer", action = "DeleteOrder" }
    );

	endpoints.MapControllerRoute(
	name: "Customer",
	pattern: "Customer/GetOrderDetails/{maHD}",
	defaults: new { controller = "Customer", action = "GetOrderDetails" }
);
    endpoints.MapControllerRoute(
name: "Signup",
pattern: "{controller=Signup}/{action=signup}/{id?}"

);


});

/*Các đường link:
 default: Trang Login
 /Order/PlaceOrder: Trang đặt đồ ăn cho khách hàng
 /Customer/index: Trang quản lý đơn hàng
/Signup/signup: Đăng ký
*/




app.Run();
