
using Akasia.Application.DTO;
using Akasia.Application.Service;
using Akasia.Domain.Entity;
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
        private IBlogPostAppService _blogPostService;
        public BlogPostService(ILogger<BlogPostService> logger, IBlogPostAppService blogPostService)
        {
            _logger = logger;
            _blogPostService = blogPostService;
        }

        public async override Task<CreateBlogPostResponse> CreateBlogPost(CreateBlogPostRequest request, ServerCallContext context)
        {
            var newPost = new CreateBlogPostRequestDTO
            {
                Title = request.Title,
                Content = request.Content
            };

            var newId = await _blogPostService.CreateAsync(newPost);

            return new CreateBlogPostResponse
            {
                NewId = newId,
            };
        }
    }
}
