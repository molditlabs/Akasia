using Akasia.Domain.Entity;
using Akasia.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Akasia.Application.DTO
{
    public class CreateBlogPostRequestDTO
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime PostDate { get; set; }
        public PostStatus Status { get; set; }
    }

    public class CreateBlogPostResponseDTO
    {
        public int Id { get; set; }
    }

    public class ReadBlogPostResponseDTO
    {
        public string Title { get; set; }
        public string Content { get; set; }
    }

}
