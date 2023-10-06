using HermesChat_TeamA.Areas.Identity.Data;
using HermesChat_TeamA.Areas.Identity.Data.Models;
using Microsoft.EntityFrameworkCore;
using HermesChat_TeamA.Hubs;

namespace HermesChat_TeamA
{
    public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);
			var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

			builder.Services.AddDbContext<HermesChatDbContext>(options => options.UseSqlServer(connectionString));

			builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = false).AddEntityFrameworkStores<HermesChatDbContext>();

			// Add services to the container.
			builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
			builder.Services.AddRazorPages();
			builder.Services.AddSignalR();
            builder.Services.AddSingleton<ListOfGroupsRepository>(); 
            builder.WebHost.UseStaticWebAssets();


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
			app.MapHub<SyncHub>("/SyncHub");
			app.MapHub<ConnectedUsersHub>("/ConnectedUsersHub");
       
           
            app.MapRazorPages();
	
			
			app.Run();
		}
	}
}