using LensUp.GalleryService.Application;
using LensUp.GalleryService.Infrastructure;
using LensUp.GalleryService.Infrastructure.Hubs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddApplication(builder.Configuration)
    .AddInfrastructure(builder.Configuration);

builder.Services.AddCors();
builder.Services.AddSignalR();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// TODO: generate https certificate
if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

// TODO: Adjust on finish
app.UseCors(options => options
    .AllowAnyMethod()
    .SetIsOriginAllowed((host) => true)
    .AllowAnyHeader()
    .AllowCredentials());

app.UseExceptionHandler();

app.MapHub<GalleryHub>("/hubs/gallery");

app.MapControllers();

app.Run();
