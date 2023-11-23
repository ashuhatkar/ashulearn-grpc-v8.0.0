/*--****************************************************************************
  --* Project Name    : Interceptor
  --* Reference       : Grpc.Core, Grpc.Server
  --* Description     : Represents Grpc service
  --* Configuration Record
  --* Review            Ver  Author           Date      Cr       Comments
  --* 001               001  A HATKAR         10/11/23  CR-XXXXX Original
  --****************************************************************************/
using Grpc.Core;
using Grpc.Server;

namespace Grpc.Server.Services;

/// <summary>
/// Represents a greeter service
/// </summary>
public partial class GreeterService : Greeter.GreeterBase
{
    #region Fields

    private readonly ILogger<GreeterService> _logger;

    #endregion

    #region Ctor

    public GreeterService(ILogger<GreeterService> logger)
    {
        _logger = logger;
    }

    #endregion

    #region Methods

    public override Task<HelloReply> SayHello(HelloRequest request,
        ServerCallContext context)
    {
        _logger.LogInformation($"Sending hello to {request.Name}");

        return Task.FromResult(new HelloReply { Message = "Hello " + request.Name });
    }

    public override async Task SayHellos(HelloRequest request,
        IServerStreamWriter<HelloReply> responseStream,
        ServerCallContext context)
    {
        var i = 0;
        while (!context.CancellationToken.IsCancellationRequested)
        {
            var message = $"How are you {request.Name}? {i++}";
            _logger.LogInformation($"Sending greeting {message}.");

            await responseStream.WriteAsync(new HelloReply { Message = message });

            await Task.Delay(1000);
        }
    }

    public override async Task<HelloReply> SayHelloToAll(IAsyncStreamReader<HelloRequest> requestStream,
        ServerCallContext context)
    {
        var names = new List<string>();
        await foreach (var request in requestStream.ReadAllAsync())
        {
            names.Add(request.Name);
        }

        var message = $"Hello {string.Join(", ", names)}";

        _logger.LogInformation($"Sending greeting {message}.");
        return new HelloReply { Message = message };
    }

    public override async Task SayHellosToAll(IAsyncStreamReader<HelloRequest> requestStream,
        IServerStreamWriter<HelloReply> responseStream,
        ServerCallContext context)
    {
        await foreach (var request in requestStream.ReadAllAsync())
        {
            await responseStream.WriteAsync(new HelloReply { Message = "Hello " + request.Name });
        }
    }

    #endregion
}