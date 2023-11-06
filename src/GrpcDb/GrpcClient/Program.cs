using Grpc.Net.Client;
using GrpcGreeter.Protos;

//From Database
using var channel = GrpcChannel.ForAddress("https://localhost:7278");
var studentClient = new RemoteStudent.RemoteStudentClient(channel);

//hard coded for test. Should be avoided
var studentInput = new StudentLookupModel { StudentId = 5 };
Console.WriteLine($"grpc request student identifier {studentInput.StudentId}");

var studentReply = await studentClient.GetStudentInfoAsync(studentInput);
Console.WriteLine($"grpc response First name: {studentReply.FirstName}, Last name: {studentReply.LastName}");

channel.ShutdownAsync().Wait();
Console.WriteLine("Press any key to exit...");
Console.ReadKey();
