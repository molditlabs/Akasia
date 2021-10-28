using Akasia.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Akasia.Application.Repository
{
    public interface IBlogPostRepository : IBaseRepository<BlogPost>
    {
        Task<bool> CheckTitleExistAsync(string title);
    }
}
