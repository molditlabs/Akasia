using Akasia.Grpc;
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

            Console.WriteLine("Read all blog post");
            var blogPostList = await client.ReadAllBlogPostAsync(new Empty());

            foreach (var item in blogPostList.BlogPostModel)
            {
                Console.WriteLine($"{item.Title} - {item.Content}");
            }

            Console.WriteLine();

            Console.WriteLine("Read all blog post Stream");
            using (var blogPostListStream = client.ReadAllBlogPostStream(new Empty()))
            {
                while (await blogPostListStream.ResponseStream.MoveNext())
                {
                    var item = blogPostListStream.ResponseStream.Current;

                    Console.WriteLine($"{item.Title} - {item.Content}");
                }
            }

            Console.WriteLine();

            Console.WriteLine("Read blog post by Id");
            var readBlogPostModelByIdRequest = new ReadBlogPostByIdRequest
            {
                Id = 1
            };

            var blogPost = client.ReadBlogPostById(readBlogPostModelByIdRequest);

            if (blogPost.BlogPostModel != null)
            {
                Console.WriteLine($"{blogPost.BlogPostModel.Title} - {blogPost.BlogPostModel.Content}");
            }
            else
            {
                Console.WriteLine("Blog not found!");
            }

            //Console.WriteLine();
            //Console.WriteLine("Create Blog post");
            //var createPostContent = new BlogPostModel
            //{
            //    Title = "Election day",
            //    Content = "Who won?",
            //};
            //var createPostResponse = await client.CreateBlogPostAsync(new CreateBlogPostRequest { BlogPostModel = createPostContent });
            //Console.WriteLine($"Message: {createPostResponse.Message}");
            //Console.WriteLine($"IsOkay: {createPostResponse.IsOkay}");

            //Console.WriteLine();

            //Console.WriteLine("Update Blog post");
            //var updateBase = new BaseProperty
            //{
            //    Id = 5
            //};
            //var updateContent = new BlogPostModel
            //{
            //    BaseProperty = updateBase,
            //    Title = "Updated title",
            //    Content = "Updated content",
            //};
            //var updatePostResponse = await client.UpdateBlogPostAsync(new UpdateBlogPostRequest { BlogPostModel = updateContent });
            //Console.WriteLine($"Message: {updatePostResponse.Message}");
            //Console.WriteLine($"IsOkay: {updatePostResponse.IsOkay}");

            Console.WriteLine();

            Console.WriteLine("Delete Blog post");
            var deletePostResponse = await client.DeleteBlogPostAsync(new DeleteBlogPostRequest { Id = 1002 });
            Console.WriteLine($"Message: {deletePostResponse.Message}");
            Console.WriteLine($"IsOkay: {deletePostResponse.IsOkay}");



            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
