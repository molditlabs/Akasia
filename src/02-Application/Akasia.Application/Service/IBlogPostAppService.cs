using Akasia.Application.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Akasia.Application.Service
{
    public interface IBlogPostAppService
    {
        // Transaction
        Task<int> CreateAsync(CreateBlogPostRequestDTO request);
       
    }
}
