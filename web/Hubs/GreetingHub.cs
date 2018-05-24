using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace signal_core.Hubs{
    //[Authorize]
    public class GreetingHub : Hub {

        public override async Task OnConnectedAsync(){
            await Clients.All.SendAsync("SendAction", Context.User.Identity.Name, "joined");
        }

        public override async Task  OnDisconnectedAsync(System.Exception exception){
            await Clients.All.SendAsync("SendAction", Context.User.Identity.Name, "left");
        }
        public void Greet(string message){
            Clients.All.SendAsync("greet",message);
        }

          public void Send(string message){
            Clients.All.SendAsync("SendMessage",message);
        }

    }
}