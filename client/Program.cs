using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Toolkit.Uwp.Notifications;

var connection = new HubConnectionBuilder()
	.WithUrl("http://localhost:5194/snakhub")
	.WithAutomaticReconnect()
	.Build();

// Receive messages from server
connection.On<string, string>("ReceiveMessage", (user, message) =>
{
	Console.WriteLine($"\n{user}: {message}");
	Console.Write("> ");

	new ToastContentBuilder()
	   .AddText($"{user}")
	   .AddText(message)
	   .Show();
});

Console.Write("Enter your username: ");
var username = Console.ReadLine() ?? "Anonymous";

try
{
	Console.WriteLine("Connecting to server...");
	await connection.StartAsync();
	Console.WriteLine("Connected to chat!");
}
catch (Exception ex)
{
	Console.WriteLine($"Failed to connect: {ex.Message}");
	Console.WriteLine("Make sure the server is running!");
	return;
}

while (true)
{
	Console.Write("> ");
	var input = Console.ReadLine();

	if (string.IsNullOrEmpty(input)) continue;
	if (input == "/quit") break;

	try
	{
		await connection.SendAsync("SendMessage", username, input);
	}
	catch (Exception ex)
	{
		Console.WriteLine($"Error sending message: {ex.Message}");
	}
}

await connection.StopAsync();