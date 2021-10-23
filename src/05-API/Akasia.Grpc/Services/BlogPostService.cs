using Akasia.Application.DTO;
using Akasia.Application.Service;
using Akasia.Domain.Entity;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Akasia.Grpc.Services
{
    public class BlogPostService : BlogPostEndpoint.BlogPostEndpointBase
    {
        private readonly ILogger<BlogPostService> _logger;
        private IBlogPostAppService _blogPostAppService;
        public BlogPostService(ILogger<BlogPostService> logger, IBlogPostAppService blogPostAppService)
        {
            _logger = logger;
            _blogPostAppService = blogPostAppService;
        }

        public async override Task<CreateBlogPostResponse> CreateBlogPost(CreateBlogPostRequest request, ServerCallContext context)
        {
            var newPost = new CreateBlogPostRequestDTO
            {
                Title = request.Title,
                Content = request.Content
            };

            var newId = await _blogPostAppService.CreateAsync(newPost);

            return new CreateBlogPostResponse
            {
                NewId = newId,
            };
        }


        public async override Task<ReadBlogPostResponse> ReadBlogPost(Empty request, ServerCallContext context)
        {
            var blogPostList = await _blogPostAppService.ReadAsync();

            var res = new ReadBlogPostResponse();
            
            foreach (var item in blogPostList)
            {
                res.BlogPostObject.Add(
                    new BlogPostObject
                    {
                        Title = item.Title,
                        Content = item.Content
                    }
                    );
                
            }

            return res;
        }

        public async override Task ReadBlogPostStream(Empty request, IServerStreamWriter<BlogPostObject> responseStream, ServerCallContext context)
        {
            var blogPostList = await _blogPostAppService.ReadAsync();

            var res = new List<BlogPostObject>();

            foreach (var item in blogPostList)
            {
                res.Add(
                        new BlogPostObject
                        { 
                            Title = item.Title,
                            Content = item.Content
                        }
                    );
            }

            foreach (var item in res)
            {
                await responseStream.WriteAsync(item);
            }
        }

        public async override Task<BlogPostObject> ReadBlogPostById(ReadBlogPostByIdRequest request, ServerCallContext context)
        {
            var blogPostDto = await _blogPostAppService.ReadByIdAsync(request.NewId);

            return new BlogPostObject
            {
                Title = blogPostDto.Title,
                Content = blogPostDto.Content
            };
        }

        public async override Task<BlogPostObject> UpdateBlogPost(UpdateBlogPostRequest request, ServerCallContext context)
        {
            var req = new UpdateBlogPostRequestDTO
            {
                Id = request.NewId,
                Title = request.Title,
                Content = request.Content
            };
            await _blogPostAppService.UpdateAsync(req);
            //var upd = await _blogPostAppService.UpdateAsync(req);

            var blogPostDto = await _blogPostAppService.ReadByIdAsync(req.Id);

            return new BlogPostObject
            {
                Title = blogPostDto.Title,
                Content = blogPostDto.Content
            };
        }
    }
}
