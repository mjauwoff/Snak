using Microsoft.AspNetCore.SignalR.Client;

var connection = new HubConnectionBuilder()
	.WithUrl("http://localhost:1111/snakhub")
	.Build();

// Receive messages from server
connection.On<string, string>("ReceiveMessage", (user, message) =>
{
	Console.WriteLine($"{user}: {message}");
});

Console.Write("Enter your username: ");
var username = Console.ReadLine() ?? "Anonymous";

await connection.StartAsync();
Console.WriteLine("Connected to chat!");

while (true)
{
	Console.Write("> ");
	var input = Console.ReadLine();

	if (string.IsNullOrEmpty(input)) continue;
	if (input == "/quit") break;

	// Send message with username
	await connection.SendAsync("SendMessage", username, input);
}

await connection.StopAsync();