using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace CMCS.Hubs
{

    public class ClaimHub : Hub
    {
        public async Task SendClaimUpdate(Claim claim)
        {
            await Clients.All.SendAsync("ReceiveClaimUpdate", claim);
        }
    }
}
