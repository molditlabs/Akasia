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

        //public async override Task<BlogPostObject> ReadBlogPostById(ReadBlogPostByIdRequest request, ServerCallContext context)
        //{
        //    var blogPostDto = await _blogPostAppService.ReadByIdAsync(request.NewId);

        //    return new BlogPostObject
        //    {
        //        Title = blogPostDto.Title,
        //        Content = blogPostDto.Content
        //    };
        //}

        

        public async override Task<ReadAllBlogPostResponse> ReadAllBlogPost(Empty request, ServerCallContext context)
        {
            _logger.LogInformation("Reading all Blog posts");

            ReadAllBlogPostResponse response = new ReadAllBlogPostResponse();

            try
            {
                var blogPostListDto = await _blogPostAppService.ReadAllAsync();

                // Map BlogPostModelDTO object to BlogPostModel object
                var blogPostModel = new List<BlogPostModel>();
                foreach (var item in blogPostListDto.BlogPostModelList)
                {
                    blogPostModel.Add
                        (
                            new BlogPostModel
                            { 
                                Title = item.Title,
                                Content = item.Content
                            }
                        );
                }

                // Add list of BlogPostModel object to blogPostModel property of BlogPostModelListResponse object
                foreach (var item in blogPostModel)
                {
                    response.BlogPostModel.Add(item);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(@$"Error: {ex.Message}");
            }

            return response;
        }

        public async override Task ReadAllBlogPostStream(Empty request, IServerStreamWriter<BlogPostModel> responseStream, ServerCallContext context)
        {
            _logger.LogInformation("Reading all Blog posts (STREAM)");

            try
            {
                var blogPostListDto = await _blogPostAppService.ReadAllAsync();

                // Map BlogPostModelDTO object to BlogPostModel object
                var blogPostModel = new List<BlogPostModel>();
                foreach (var item in blogPostListDto.BlogPostModelList)
                {
                    blogPostModel.Add
                        (
                            new BlogPostModel
                            {
                                Title = item.Title,
                                Content = item.Content
                            }
                        );
                }

                // Fetch list of BlogPostModel object
                foreach (var item in blogPostModel)
                {
                    await responseStream.WriteAsync(item);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(@$"Error: {ex.Message}");
            }
        }

        public async override Task<TransactionResponse> CreateBlogPost(CreateBlogPostRequest request, ServerCallContext context)
        {
            var blogPostExist = await _blogPostAppService.IsRecordExist(request.BlogPostModel.Title);

            if (blogPostExist == true)
            {
                return new TransactionResponse
                {
                    Message = "Request already exists.",
                    IsOkay = false
                };
            }

            var newPost = new CreateBlogPostRequestDTO
            {
                Title = request.BlogPostModel.Title,
                Content = request.BlogPostModel.Content
            };

            _logger.LogInformation("Creating new Post");

            try
            {
                var newId = await _blogPostAppService.CreateAsync(newPost);
            }
            catch (Exception ex)
            {
                _logger.LogError(@$"Error: {ex.Message}");
            }

            return new TransactionResponse
            {
                Message = "Request succesfully created.",
                IsOkay = true
            };
        }

        public async override Task<TransactionResponse> UpdateBlogPost(UpdateBlogPostRequest request, ServerCallContext context)
        {
            var blogPostExist = await _blogPostAppService.IsRecordExist(request.BlogPostModel.Title);

            if (blogPostExist == false)
            {
                return new TransactionResponse
                {
                    Message = "Request not exists.",
                    IsOkay = false
                };
            }

            var updatePost = new UpdateBlogPostRequestDTO
            {
                Id = request.BlogPostModel.BaseProperty.Id,
                Title = request.BlogPostModel.Title,
                Content = request.BlogPostModel.Content
            };

            _logger.LogInformation("Updating Post");

            try
            {
                await _blogPostAppService.UpdateAsync(updatePost);
            }
            catch (Exception ex)
            {
                _logger.LogError(@$"Error: {ex.Message}");
            }

            return new TransactionResponse
            {
                Message = "Request succesfully updated.",
                IsOkay = true
            };
        }
    }
}
