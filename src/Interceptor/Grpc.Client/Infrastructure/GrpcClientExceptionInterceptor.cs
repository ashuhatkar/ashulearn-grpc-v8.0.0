﻿/*--****************************************************************************
  --* Project Name    : Interceptor
  --* Reference       : Grpc.Core, Grpc.Core.Interceptors, Microsoft.Extensions.Logging
  --* Description     : Represents Grpc client exception interceptor
  --* Configuration Record
  --* Review            Ver  Author           Date      Cr       Comments
  --* 001               001  A HATKAR         10/11/23  CR-XXXXX Original
  --****************************************************************************/
using Grpc.Core;
using Grpc.Core.Interceptors;
using Microsoft.Extensions.Logging;

namespace Grpc.Client.Infrastructure
{
    /// <summary>
    /// Represents a grpc client exception interceptor
    /// </summary>
    public class GrpcClientExceptionInterceptor : Interceptor
    {
        #region Fields

        private readonly ILogger<GrpcClientExceptionInterceptor> _logger;

        #endregion

        #region Ctor

        public GrpcClientExceptionInterceptor(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<GrpcClientExceptionInterceptor>();
        }

        #endregion

        #region Utilities

        private async Task<TResponse> HandleResponse<TResponse>(Task<TResponse> task)
        {
            try
            {
                var response = await task;
                _logger.LogInformation($"Response received: {response}");
                return response;
            }
            catch (RpcException ex)
            {
                LogError(ex);
                return default;
            }
        }

        private void LogCall<TRequest, TResponse>(Method<TRequest, TResponse> method)
            where TRequest : class
            where TResponse : class
        {
            _logger.LogInformation($"Starting call. Name: {method.Name}. Type: {method.Type}. Request: {typeof(TRequest)}. Response: {typeof(TResponse)}");
        }

        private void LogError(RpcException ex)
        {
            _logger.LogError(ex, "Error calling via gRPC: {message}, {status}", ex.Message, ex.Status);
        }

        #endregion

        #region Methods

        public override AsyncUnaryCall<TResponse> AsyncUnaryCall<TRequest, TResponse>(TRequest request,
            ClientInterceptorContext<TRequest, TResponse> context,
            AsyncUnaryCallContinuation<TRequest, TResponse> continuation)
        {
            LogCall(context.Method);

            try
            {
                var call = continuation(request, context);

                return new AsyncUnaryCall<TResponse>(HandleResponse(call.ResponseAsync), call.ResponseHeadersAsync, call.GetStatus, call.GetTrailers, call.Dispose);
            }
            catch (RpcException ex)
            {
                LogError(ex);
                throw;
            }
        }

        public override AsyncClientStreamingCall<TRequest, TResponse> AsyncClientStreamingCall<TRequest, TResponse>(
            ClientInterceptorContext<TRequest, TResponse> context,
            AsyncClientStreamingCallContinuation<TRequest, TResponse> continuation)
        {
            LogCall(context.Method);

            try
            {
                return continuation(context);
            }
            catch (RpcException ex)
            {
                LogError(ex);
                throw;
            }
        }

        public override AsyncServerStreamingCall<TResponse> AsyncServerStreamingCall<TRequest, TResponse>(TRequest request,
            ClientInterceptorContext<TRequest, TResponse> context,
            AsyncServerStreamingCallContinuation<TRequest, TResponse> continuation)
        {
            LogCall(context.Method);

            try
            {
                return continuation(request, context);
            }
            catch (RpcException ex)
            {
                LogError(ex);
                throw;
            }
        }

        public override AsyncDuplexStreamingCall<TRequest, TResponse> AsyncDuplexStreamingCall<TRequest, TResponse>(
            ClientInterceptorContext<TRequest, TResponse> context,
            AsyncDuplexStreamingCallContinuation<TRequest, TResponse> continuation)
        {
            LogCall(context.Method);

            try
            {
                return continuation(context);
            }
            catch (RpcException ex)
            {
                LogError(ex);
                throw;
            }
        }

        public override TResponse BlockingUnaryCall<TRequest, TResponse>(TRequest request,
            ClientInterceptorContext<TRequest, TResponse> context,
            BlockingUnaryCallContinuation<TRequest, TResponse> continuation)
        {
            LogCall(context.Method);

            try
            {
                return continuation(request, context);
            }
            catch (RpcException ex)
            {
                LogError(ex);
                throw;
            }
        }

        #endregion
    }
}