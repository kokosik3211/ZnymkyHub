
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace ZnymkyHub.Hubs
{
    public class ForumHub: Hub
    {
        public async Task Send(string message)
        {
            await this.Clients.All.SendAsync("Send", message);
        }
    }
}
