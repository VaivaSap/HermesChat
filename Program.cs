using HermesChat_TeamA.Areas.Identity.Data;
using HermesChat_TeamA.Data;
using Microsoft.EntityFrameworkCore;

namespace HermesChat_TeamA
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);
			var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

			builder.Services.AddDbContext<HermesChat_TeamA.Data.HermesChatDbContext>(options => options.UseSqlServer(connectionString));

			builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = false).AddEntityFrameworkStores<HermesChat_TeamA.Data.HermesChatDbContext>();

			// Add services to the container.
			builder.Services.AddControllersWithViews();
			builder.Services.AddRazorPages();

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

			app.MapControllerRoute(
				name: "default",
 				pattern: "{controller=Home}/{action=Index}/{id?}");
			app.MapRazorPages();
			app.Run();
		}
	}
}