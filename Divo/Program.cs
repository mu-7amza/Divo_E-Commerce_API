using DAL.Contexts;
using DAL.Utility.DataSeed;
using Divo.Extensions;
using Divo.Middleware;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);

builder.Services.AddAppServices(builder.Configuration);

var app = builder.Build();

app.UseMiddleware<ExceptionMiddlware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStatusCodePagesWithReExecute("/error/{0}");

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthentication();

app.UseCors("CorsPolicy");

app.UseAuthorization();


app.MapControllers();

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
var context = services.GetRequiredService<AppDbContext>();
var logger = services.GetRequiredService<ILogger<Program>>();
try
{
    context.Database.Migrate();
    await SeedData.SeedDatabaseAsynce(context);
}
catch(Exception ex)
{
    logger.LogError(ex, "An error occurred while migrating the database.");
}

app.Run();
