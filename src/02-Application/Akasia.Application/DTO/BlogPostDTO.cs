using Akasia.Domain.Entity;
using Akasia.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Akasia.Application.DTO
{
    public class BlogPostBaseModelDTO
    {
        // Key
        public int Id { get; set; }
        // Row Attribute
        public bool IsDeleted { get; set; }
        // Audit Fields
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }

    public class BlogPostModelDTO : BlogPostBaseModelDTO
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime PostDate { get; set; }
        public PostStatus Status { get; set; }
    }

    public class ReadAllBlogPostResponseDTO
    {
        public List<BlogPostModelDTO> BlogPostModelList { get; set; } = new List<BlogPostModelDTO>();
    }
    public class ReadBlogPostByIdResponseDTO
    {
        public BlogPostModelDTO BlogPostModelDTO { get; set; }
    }
    public class CreateBlogPostRequestDTO : BlogPostModelDTO
    {

    }
    public class UpdateBlogPostRequestDTO : BlogPostModelDTO
    {
        
    }
}
