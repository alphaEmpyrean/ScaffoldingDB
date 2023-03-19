using Microsoft.EntityFrameworkCore;
using ScaffoldingDB.Data;

namespace ScaffoldingDB
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();

            builder.Services.AddDbContext<RediRndContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("rediRND")));

            // Add db test class to DI container
            builder.Services.AddScoped<TestDbActions>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();

            // Test accessing the db
            using ( var serviceScope = app.Services.CreateScope())
            {
                var db = serviceScope.ServiceProvider.GetService<RediRndContext>();
                if (db != null)
                {
                    TestDbActions actions = new(db);
                    actions.Run();
                }               
            }
    

            app.Run();
        }
    }
}