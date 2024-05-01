using Domain.Interfaces;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Serices_Applications.Data;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
	options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
	.AddRoles<IdentityRole>()
	.AddEntityFrameworkStores<ApplicationDbContext>();


builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<ITempDataProvider, CookieTempDataProvider>();
builder.Services.AddSession();

// adding the Dependancy Injection refrences .
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IServicesRepository, SerevicesRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseMigrationsEndPoint();
}
else
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();
app.UseAuthentication();;

app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=services}/{action=Index}/{id?}");
app.MapRazorPages();

//Create service to initalize the roles. 
using (var scope = app.Services.CreateScope())
{
	var rolemanager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

	var roles = new[] { "super_admin", "admin","editor" ,"user" };

	foreach (var role in roles)
	{
		if (!await rolemanager.RoleExistsAsync(role))
		{
			await rolemanager.CreateAsync(new IdentityRole (role));
		}
	}

}
//  adding a default user to the system.
using (var scope = app.Services.CreateScope())
{
	var usermanager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

	string email = "m@m.com";
	string password = "Mm@123";

	if (await usermanager.FindByEmailAsync(email) == null)
	{
		var user = new IdentityUser();
		user.Email = email;
		user.UserName = email;
		user.EmailConfirmed = true;

		await usermanager.CreateAsync(user,password);

		await usermanager.AddToRoleAsync(user, "super_Admin");
	}

}


app.Run();
