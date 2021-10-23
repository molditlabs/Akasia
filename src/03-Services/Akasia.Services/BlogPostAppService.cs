using Akasia.Application;
using Akasia.Application.DTO;
using Akasia.Application.Service;
using Akasia.Domain.Entity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Akasia.Services
{
    public class BlogPostAppService : IBlogPostAppService
    {
        private readonly IUnitOfWork _unitofwork;
        private readonly ILogger<BlogPostAppService> _logger;

        public BlogPostAppService(IUnitOfWork unitofwork, ILogger<BlogPostAppService> logger)
        {
            _unitofwork = unitofwork;
            _logger = logger;
        }

        public async Task<int> CreateAsync(CreateBlogPostRequestDTO request)
        {
            try
            {
                var blogPost = new BlogPost
                {
                    Title = request.Title,
                    Content = request.Content
                };

                _unitofwork.CreateTransaction();
                await _unitofwork.BlogPost.CreateAsync(blogPost);
                _unitofwork.Commit();

                return blogPost.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(@$"Error: {ex.Message}" );
                _unitofwork.Rollback();
                return 0;
            }
        }

        public async Task<List<ReadBlogPostResponseDTO>> ReadAsync()
        {
            try
            {
                _unitofwork.CreateTransaction();
                var blogPostListResponse = await _unitofwork.BlogPost.ReadAsync();
                _unitofwork.Commit();

                var dto = new List<ReadBlogPostResponseDTO>();
                foreach (var blogPost in blogPostListResponse)
                {
                    dto.Add(
                            new ReadBlogPostResponseDTO 
                            {
                                Title = blogPost.Title,
                                Content = blogPost.Content
                            }
                        );
                }

                return dto;
            }
            catch (Exception ex)
            {
                _logger.LogError(@$"Error: {ex.Message}");
                _unitofwork.Rollback();
                return null;
            }
        }
    }
}
