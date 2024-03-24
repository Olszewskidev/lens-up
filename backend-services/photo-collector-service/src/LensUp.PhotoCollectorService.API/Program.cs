using LensUp.PhotoCollectorService.API.Channels;
using LensUp.PhotoCollectorService.API.Requests;
using LensUp.PhotoCollectorService.API.Services;
using LensUp.PhotoCollectorService.API.Validators;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddAntiforgery()
    .AddScoped<IUploadPhotoToGalleryRequestValidator, UploadPhotoToGalleryRequestValidator>()
    .AddSingleton<IPhotoProcessor, PhotoProcessor>()
    .AddSingleton<IPhotoChannel, PhotoChannel>()
    .AddHostedService<BackgroundPhotoProcessor>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAntiforgery();

app.MapPost("/upload-photo/{enterCode}", async (
    int enterCode,
    [FromForm] UploadPhotoToGalleryRequest request, 
    IUploadPhotoToGalleryRequestValidator validator, 
    IPhotoChannel channel) =>
{

    validator.EnsureThatPhotoFileIsValid(request.PhotoFile);
    // await validator.EnsureThatGalleryIsActivated(enterCode);    

    await channel.PublishAsync(request);
    return Results.Accepted();
}) 
.DisableAntiforgery() // Need this when you want to upload without a token
.WithName("AddPhotoToGallery")
.WithOpenApi();

app.Run();
