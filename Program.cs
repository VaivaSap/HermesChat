using HermesChat_TeamA.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using HermesChat_TeamA.Areas.Identity.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using HermesChat_TeamA.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace HermesChat_TeamA
{
    public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);
			var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

			builder.Services.AddDbContext<HermesChatDbContext>(options => options.UseSqlServer(connectionString));

			builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<HermesChat_TeamA.Areas.Identity.Data.HermesChatDbContext>();
         

            builder.Services.AddControllers();
            builder.Services.Configure<SmtpSettings>(options => builder.Configuration.GetSection("EmailConfiguration")); 
            
          builder.Services.AddTransient<IEmailSender, EmailSender>();

          

            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Example API",
                    Version = "v1",
                    Description = "An example of an ASP.NET Core Web API",
                    Contact = new OpenApiContact
                    {
                        Name = "Example Contact",
                        Email = "example@example.com",
                        Url = new Uri("https://example.com/contact"),
                    },
                });
			//builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<HermesChatDbContext>();

            });
            // Add services to the container.
            builder.Services.AddControllersWithViews();
			// Add services to the container.
			builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
			builder.Services.AddRazorPages();
            builder.Services.AddSingleton<IListOfGroupsRepository, ListOfGroupsRepository>(); 
           // builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
               // .AddCookie(option =>
                //{
                 //   option.LoginPath = "/Home/Index";
                 //   option.ExpireTimeSpan = TimeSpan.FromMinutes(20);
                //});
			builder.Services.AddSignalR();
            builder.WebHost.UseStaticWebAssets();

            //builder.Services.AddServerSideBlazor().AddHubOptions(options => {
            //    options.MaximumReceiveMessageSize = null; // no limit or use a number
            //});

            var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();

                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                });
            }

            app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();
            app.UseAuthentication();
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