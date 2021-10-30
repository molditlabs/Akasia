using Akasia.Application;
using Akasia.Application.DTO;
using Akasia.Application.Service;
using Akasia.Domain.Entity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<bool> CheckTitleExistAsync(string title)
        {
            bool isExist = false;

            try
            {
                _unitofwork.CreateTransaction();
                isExist = await _unitofwork.BlogPost.CheckTitleExistAsync(title);
                _unitofwork.Commit();
            }
            catch (Exception ex)
            {
                _logger.LogError(@$"Error: {ex.Message}");
                _unitofwork.Rollback();
            }

            return isExist;
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

        public async Task DeleteAsync(int id)
        {
            try
            {
                _unitofwork.CreateTransaction();
                await _unitofwork.BlogPost.DeleteAsync(id);
                _unitofwork.Commit();

            }
            catch (Exception ex)
            {
                _logger.LogError(@$"Error: {ex.Message}");
                _unitofwork.Rollback();

            }
        }

        public async Task<bool> IsRecordExist(int id)
        {
            bool isExist = false;

            try
            {
                _unitofwork.CreateTransaction();
                isExist = await _unitofwork.BlogPost.IsRecordExistAsync(id);
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
            ReadAllBlogPostResponseDTO blogPostDtoList = new ReadAllBlogPostResponseDTO();
            try
            {
                _unitofwork.CreateTransaction();
                var blogPostList = await _unitofwork.BlogPost.ReadAllAsync();
                _unitofwork.Commit();

                // Map list of BlogPost object to BlogPostModelDTO object and add to list
                if (blogPostList.Any())
                {
                    var blogPostDto = new List<BlogPostModelDTO>();
                    foreach (var item in blogPostList)
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
                        blogPostDtoList.BlogPostModelList.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(@$"Error: {ex.Message}");
                _unitofwork.Rollback();
            }

            return blogPostDtoList;
        }

        public async Task<ReadBlogPostByIdResponseDTO> ReadByIdAsync(int id)
        {
            ReadBlogPostByIdResponseDTO blogPostDto = new ReadBlogPostByIdResponseDTO();

            try
            {
                _unitofwork.CreateTransaction();
                var blogPost = await _unitofwork.BlogPost.ReadByIdAsync(id);
                _unitofwork.Commit();

                // If blog post exists, map BlogPost object to BlogPostModelDTO object
                if (blogPost != null)
                {
                    var blogPostModelDto = new BlogPostModelDTO
                    { 
                        Title = blogPost.Title,
                        Content = blogPost.Content
                    };

                    blogPostDto.BlogPostModelDTO = blogPostModelDto;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(@$"Error: {ex.Message}");
                _unitofwork.Rollback();
            }

            return blogPostDto;
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
