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

        public async Task<bool> IsRecordExist(string title)
        {
            bool isExist = false;

            try
            {
                _unitofwork.CreateTransaction();
                isExist = await _unitofwork.BlogPost.IsRecordExistAsync(title);
                _unitofwork.Commit();
            }
            catch (Exception ex)
            {
                _logger.LogError(@$"Error: {ex.Message}");
                _unitofwork.Rollback();
            }

            return isExist;
        }

        public async Task<ReadAllBlogPostResponseDTO> ReadAllAsync()
        {
            ReadAllBlogPostResponseDTO blogPostListDto = new ReadAllBlogPostResponseDTO();
            try
            {
                _unitofwork.CreateTransaction();
                var blogPostListResponse = await _unitofwork.BlogPost.ReadAllAsync();
                _unitofwork.Commit();

                // Map list of BlogPost object to BlogPostModelDTO object and add to list
                var blogPostDto = new List<BlogPostModelDTO>();
                foreach (var item in blogPostListResponse)
                {
                    blogPostDto.Add
                        (
                            new BlogPostModelDTO
                            {
                                Title = item.Title,
                                Content = item.Content
                            }
                        );
                }

                // Add list of BlogPostModelDTO object to BlogPostModelList property of ReadAllBlogPostDTO object 
                foreach (var item in blogPostDto)
                {
        

                    blogPostListDto.BlogPostModelList.Add(item);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(@$"Error: {ex.Message}");
                _unitofwork.Rollback();
            }

            return blogPostListDto;
        }

        public async Task<ReadBlogPostByIdResponseDTO> ReadByIdAsync(int id)
        {
            try
            {
                _unitofwork.CreateTransaction();
                var blogPost = await _unitofwork.BlogPost.ReadByIdAsync(id);
                _unitofwork.Commit();


                var blogPostDto = new ReadBlogPostByIdResponseDTO
                {
                    Title = blogPost.Title,
                    Content = blogPost.Content
                };

                return blogPostDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(@$"Error: {ex.Message}");
                _unitofwork.Rollback();
                return null;
            }
        }

        public async Task UpdateAsync(UpdateBlogPostRequestDTO request)
        {
            try
            {
                _unitofwork.CreateTransaction();
                await _unitofwork.BlogPost.UpdateAsync(request.Id, request.Title, request.Content);
                _unitofwork.Commit();

            }
            catch (Exception ex)
            {
                _logger.LogError(@$"Error: {ex.Message}");
                _unitofwork.Rollback();
                
            }
        }
    }
}
