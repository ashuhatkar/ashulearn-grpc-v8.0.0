/*--****************************************************************************
    --* Project Name    : Greeter
    --* Reference       : Grpc.Core
    --* Description     : Represents Grpc service
    --* Configuration Record
    --* Review            Ver  Author           Date      Cr       Comments
    --* 001               001  A HATKAR         10/11/23  CR-XXXXX Original
  --****************************************************************************/
using Grpc.Core;
using Grpc.Server;

namespace Grpc.Server.Services;

/// <summary>
/// Represents a greeter grpc service
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

    public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
    {
        return Task.FromResult(new HelloReply
        {
            Message = "Hello " + request.Name
        });
    }

    #endregion
}