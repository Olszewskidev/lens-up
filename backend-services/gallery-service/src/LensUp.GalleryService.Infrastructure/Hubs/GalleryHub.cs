using Microsoft.AspNetCore.SignalR;

namespace LensUp.GalleryService.Infrastructure.Hubs;

public sealed class GalleryHub : Hub
{
    public override async Task OnConnectedAsync()
    {
        if (this.Context == null)
        {
            return;
        }

        string? galleryId = this.GetGalleryId();
        if (!string.IsNullOrWhiteSpace(galleryId))
        {
            await this.Groups.AddToGroupAsync(this.Context.ConnectionId, galleryId);
        }

        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception exception)
    {
        string? galleryId = this.GetGalleryId();
        if (!string.IsNullOrWhiteSpace(galleryId))
        {
            await this.Groups.RemoveFromGroupAsync(this.Context.ConnectionId, galleryId);
        }

        await base.OnDisconnectedAsync(exception);
    }

    private string? GetGalleryId() => this.Context.GetHttpContext()?.Request.Query["galleryId"];
}
