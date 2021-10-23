


using Akasia.Grpc;
using Grpc.Core;
using Grpc.Net.Client;
using System;

namespace Akasia.ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            // The port number(5001) must match the port of the gRPC server.
            using var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new BlogPostEndpoint.BlogPostEndpointClient(channel);
            var reply = client.CreateBlogPost(
                              new CreateBlogPostRequest { Title = "Sample Dapper Post", Content="Sample Content" });
            //Console.WriteLine("BlogPost Response is: " + reply.Message);
            Console.WriteLine("BlogPost ID is: " + reply.NewId);
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
