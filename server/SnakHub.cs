using Microsoft.AspNetCore.SignalR;

public class SnakHub : Hub
{
	public async Task SendMessage(string name, string message)
	{
		await Clients.All.SendAsync("ReceiveMessage", name, message);
	}

	public override async Task OnConnectedAsync()
	{
		await Clients.All.SendAsync("ReceiveMessage", "System", $"A user connected: {Context.ConnectionId}");
		await base.OnConnectedAsync();
	}

	public override async Task OnDisconnectedAsync(Exception? exception)
	{
		await Clients.All.SendAsync("ReceiveMessage", "System", $"A user disconnected: {Context.ConnectionId}");
		await base.OnDisconnectedAsync(exception);
	}
};