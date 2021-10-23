﻿using Akasia.Grpc;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Grpc.Net.Client;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Akasia.ConsoleTest
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var httpHandler = new HttpClientHandler();
            // Return `true` to allow certificates that are untrusted/invalid
            httpHandler.ServerCertificateCustomValidationCallback =
                HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

            // The port number(5001) must match the port of the gRPC server.
            using var channel = GrpcChannel.ForAddress("https://localhost:5001", new GrpcChannelOptions { HttpHandler = httpHandler });
            var client = new BlogPostEndpoint.BlogPostEndpointClient(channel);
            //var reply = client.CreateBlogPost(
            //new CreateBlogPostRequest { Title = "Election day", Content="Who won?" });

            //Console.WriteLine("BlogPost ID is: " + reply.NewId);

            Console.WriteLine("Normal List");
            var reply = await client.ReadBlogPostAsync(new Empty());

            foreach (var item in reply.BlogPostObject)
            {
                Console.WriteLine($"{item.Title} - {item.Content}");
                Console.WriteLine();
            }

            Console.WriteLine();

            Console.WriteLine("Stream");
            using (var call = client.ReadBlogPostStream(new Empty()))
            {
                while (await call.ResponseStream.MoveNext())
                {
                    var item = call.ResponseStream.Current;

                    Console.WriteLine($"{item.Title} - {item.Content}");
                    Console.WriteLine();
                }
            }

            Console.WriteLine();

            Console.WriteLine("Read by ID");
            var read = client.ReadBlogPostById(new ReadBlogPostByIdRequest { NewId = 1});
            Console.WriteLine($"{read.Title} - {read.Content}");

            Console.WriteLine();

            Console.WriteLine("Update");
            var upd = await client.UpdateBlogPostAsync(new UpdateBlogPostRequest { NewId = 1, Title = "Very simple update", Content = "Simple update content" });
            Console.WriteLine($"{upd.Title} - {upd.Content}");

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
