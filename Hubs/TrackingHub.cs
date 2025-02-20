using Microsoft.AspNetCore.SignalR;

namespace FuelGo.Hubs
{
    public class TrackingHub : Hub
    {
        // Clients call this method to join a group for a specific order number.
        public async Task JoinOrderGroup(string orderNumber)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, $"order-{orderNumber}");
        }

        // Optional: Clients can leave the group.
        public async Task LeaveOrderGroup(string orderNumber)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"order-{orderNumber}");
        }

    }
}
