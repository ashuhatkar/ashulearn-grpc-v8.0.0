using Grpc.Client.Infrastructure;
using Grpc.Core;
using Grpc.Core.Interceptors;
using Grpc.Net.Client;
using Grpc.Server;
using Microsoft.Extensions.Logging;

var loggerFactory = LoggerFactory.Create(b => b.AddConsole());
using var channel = GrpcChannel.ForAddress("https://localhost:7006", new GrpcChannelOptions
{
    LoggerFactory = loggerFactory
});

var invoker = channel.Intercept(new GrpcClientExceptionInterceptor(loggerFactory));

var client = new Greeter.GreeterClient(invoker);

blockingUnaryCallExample(client);

await unaryCallExample(client);

await serverStreamingCallExample(client);

await clientStreamingCallAsync(client);

await bidirectionalCallExample(client);


channel.ShutdownAsync().Wait();
Console.WriteLine("Press any key to exit...");
Console.ReadKey();


static void blockingUnaryCallExample(Greeter.GreeterClient client)
{
    var reply = client.SayHello(new HelloRequest { Name = "GreeterClient" });
    Console.WriteLine("Greeting: " + reply.Message);
}

static async Task unaryCallExample(Greeter.GreeterClient client)
{
    var reply = await client.SayHelloAsync(new HelloRequest { Name = "GreeterClient" });
    Console.WriteLine("Greeting: " + reply.Message);
}

static async Task serverStreamingCallExample(Greeter.GreeterClient client)
{
    var cts = new CancellationTokenSource();
    cts.CancelAfter(TimeSpan.FromSeconds(3.5));

    using var call = client.SayHellos(new HelloRequest { Name = "GreeterClient" }, cancellationToken: cts.Token);
    try
    {
        await foreach (var message in call.ResponseStream.ReadAllAsync())
        {
            Console.WriteLine("Greeting: " + message.Message);
        }
    }
    catch (RpcException ex) when (ex.StatusCode == StatusCode.Cancelled)
    {
        Console.WriteLine("Stream cancelled.");
    }
}

static async Task clientStreamingCallAsync(Greeter.GreeterClient client)
{
    using var call = client.SayHelloToAll();
    for (var i = 0; i < 3; i++)
    {
        await call.RequestStream.WriteAsync(new HelloRequest { Name = $"GreeterClient{i + 1}" });
    }

    await call.RequestStream.CompleteAsync();
    var reply = await call;
    Console.WriteLine("Greeting: " + reply.Message);
}

static async Task bidirectionalCallExample(Greeter.GreeterClient client)
{
    using var call = client.SayHellosToAll();
    var readTask = Task.Run(async () =>
    {
        await foreach (var message in call.ResponseStream.ReadAllAsync())
        {
            Console.WriteLine("Greeting: " + message.Message);
        }
    });

    for (var i = 0; i < 3; i++)
    {
        await call.RequestStream.WriteAsync(new HelloRequest { Name = $"GreeterClient{i + 1}" });
    }

    await call.RequestStream.CompleteAsync();
    await readTask;
}