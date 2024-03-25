using LensUp.PhotoCollectorService.API;
using LensUp.PhotoCollectorService.API.Channels;
using LensUp.PhotoCollectorService.API.Requests;
using LensUp.PhotoCollectorService.API.Validators;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddAntiforgery()
    .AddValidators()
    .AddChannels()
    .AddServices();

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

    await channel.PublishAsync(new PhotoProcessorRequest(GalleryId: string.Empty, request.PhotoFile));
    return Results.Accepted();
}) 
.DisableAntiforgery() // Need this when you want to upload without a token
.WithName("AddPhotoToGallery")
.WithOpenApi();

app.Run();
