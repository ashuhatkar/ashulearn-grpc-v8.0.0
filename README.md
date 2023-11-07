# gRPC for .NET Examples

Examples of basic gRPC scenarios with gRPC for .NET.

If you are brand new to gRPC on .NET a good place to start is the getting started tutorial: [Create a gRPC client and server in ASP.NET Core](https://docs.microsoft.com/aspnet/core/tutorials/grpc/grpc-start)

## [Greeter](./Greeter)

The greeter shows how to create unary (non-streaming) gRPC methods in ASP.NET Core, and call them from a client.

##### Scenarios:

* Unary call

## [GrpcDb](./GrpcDb)

The grpcdb shows how to create database unary (non-streaming) gRPC methods in ASP.NET Core, and call them from a client.

##### Scenarios:

* Unary call

## [Interceptor](./Interceptor)

The interceptor shows how to use gRPC interceptors on the client and server. The client interceptor adds additional metadata to each call and the server interceptor logs that metadata on the server.

##### Scenarios:

* Creating a client interceptor
* Using a client interceptor
* Creating a server interceptor
* Using a server interceptor

### Contribute ###

Contributions are welcome!