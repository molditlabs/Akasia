
using Akasia.Domain.Entity;
using Akasia.Infra.Respository;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Akasia.Grpc.Services
{
    public class BlogPostService : BlogPost.BlogPostBase
    {
        private readonly ILogger<BlogPostService> _logger;
        private IBlogPostTemporaryRepository _blogPostTemporaryRepository;
        public BlogPostService(ILogger<BlogPostService> logger, IBlogPostTemporaryRepository blogPostTemporaryRepository)
        {
            _logger = logger;
            _blogPostTemporaryRepository = blogPostTemporaryRepository;
        }

        public override Task<BlogPostReply> SavePost(BlogPostRequest request, ServerCallContext context)
        {
            var newPost = new Post {
                Title = request.Title,
                Content = request.Content
            };

            var newId = _blogPostTemporaryRepository.SaveBlogPostTemporary(newPost);

            return Task.FromResult(new BlogPostReply
            {
                Message = @$"Saved Blog Post {request.Title} with ID [{newId}]",
                NewBlogId = newId,
            });
        }
    }
}
