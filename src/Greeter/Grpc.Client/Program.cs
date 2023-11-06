using Grpc.Net.Client;
using Grpc.Server;

using var channel = GrpcChannel.ForAddress("https://localhost:7209");
var client = new Greeter.GreeterClient(channel);

var request = await client.SayHelloAsync(new HelloRequest { Name = "GreeterClient" });
Console.WriteLine("grpc response: " + request.Message);

channel.ShutdownAsync().Wait();
Console.WriteLine("Press any key to exit...");
Console.ReadKey();
