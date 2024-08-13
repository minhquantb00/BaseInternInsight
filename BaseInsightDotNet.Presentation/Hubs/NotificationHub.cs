using Microsoft.AspNetCore.SignalR;

namespace BaseInsightDotNet.Presentation.Hubs
{
    public class NotificationHub : Hub
    {
        private static List<string> _connectedUsers = new List<string>();
        private string GetUserIdFromToken()
        {
            var userIdClaim = Context.User?.FindFirst("Id");
            return userIdClaim?.Value;
        }

        public async Task SendNotification(string content, string? image)
        {
            string userId = GetUserIdFromToken();
            var notification = new
            {
                UserId = userId,
                Content = content,
                Image = image,
                CreateTime = DateTime.UtcNow,
                IsSeen = false
            };

            await Clients.All.SendAsync("ReceiveNotification", notification);
        }

        public async Task SendNotificationToUser(string content, string? image)
        {
            string userId = GetUserIdFromToken();
            var notification = new
            {
                UserId = userId,
                Content = content,
                Image = image,
                CreateTime = DateTime.Now,
                IsSeen = false
            };

            await Clients.User(userId).SendAsync("ReceiveNotification", notification);
        }

        public override async Task OnConnectedAsync()
        {
            _connectedUsers.Add(Context.ConnectionId);
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            _connectedUsers.Remove(Context.ConnectionId);
            await base.OnDisconnectedAsync(exception);
        }
    }
}
