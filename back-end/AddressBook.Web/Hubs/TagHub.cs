using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace AddressBook.Web.Hubs
{
    public class TagHub : Hub
    {
        public Task Send(int tagId, string tagName)
        {
            return Clients.All.SendAsync("Send", tagId, tagName);
        }
    }
}
