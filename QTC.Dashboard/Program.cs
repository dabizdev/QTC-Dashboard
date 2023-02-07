using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation;
using Qtc.Dashboard.BusinessLayer.Configuration;
using Qtc.Dashboard.BusinessLayer;
using QTC.Dashboard.WebApp.Factories;
using QTC.Dashboard.WebApp.Interfaces;
using Dashboard.Common.Interfaces;
using Dashboard.Common.Modules;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddControllersWithViews(options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true).AddRazorRuntimeCompilation();
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

ConfigurationManager configuration = builder.Configuration;
var engineConfiguration = configuration.GetSection("EngineConfiguration").Get<EngineConfiguration>();
builder.Services.AddSingleton(engineConfiguration);

builder.Services.AddScoped<ITenantFactory, TenantFactory>();


builder.Services.AddDbContext<SqlEntities>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("rhrp")));

var assemblies = new List<Assembly>();
var directoryPath = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Assemblies");

var directories = Directory.GetDirectories(directoryPath);
foreach (var directory in directories)
{
    foreach (string assemblyPath in Directory.GetFiles(directory, "*.dll", SearchOption.AllDirectories))
    {
        var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(assemblyPath) ?? String.Empty;
        if (fileNameWithoutExtension.StartsWith("Dashboard"))
        {
            var assembly = Assembly.LoadFrom(assemblyPath);

            Type[] types = assembly.GetTypes();
            foreach (Type type in types)
            {
                if (type.GetInterfaces().Contains(typeof(ITenant)))
                {
                    IEnumerable<Attribute> attrs = type.GetCustomAttributes(typeof(DashboardModuleAttribute));
                    if (attrs.Any())
                    {
                        DashboardModuleAttribute moduleAttr = attrs.ToArray()[0] as DashboardModuleAttribute;
                        assemblies.Add(assembly);
                        Console.WriteLine("Initializing module '{0}'.", moduleAttr?.Name);
                        break;
                    }
                }
            }
        }
        else
        {
            //var clx = new CustomLoadContext(assemblyPath); // initialize custom context
            //var asm = clx.LoadFromStream(new MemoryStream(File.ReadAllBytes(assemblyPath))); // load your desired assembly
            //var assembly = Assembly.LoadFile(assemblyPath);
        }
    }
}

builder.Services.Scan(scan => scan
    .FromAssemblies(assemblies)
    .AddClasses(classes => classes.AssignableTo<ITenant>(), publicOnly: true)
    .AsImplementedInterfaces()
    .WithScopedLifetime());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();