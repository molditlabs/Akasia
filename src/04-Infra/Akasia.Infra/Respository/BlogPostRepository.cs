using Akasia.Domain.Entity;
using Akasia.Infra.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Akasia.Infra.Respository
{
    public interface IBlogPostTemporaryRepository
    {
        int SaveBlogPostTemporary(Post newPost); 
    }

    public class BlogPostTemporaryRepository : IBlogPostTemporaryRepository
    {
        private AkasiaDbContext _dbcontext;
        public BlogPostTemporaryRepository(AkasiaDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public int SaveBlogPostTemporary(Post newPost)
        {
            _dbcontext.Post.Add(newPost);
            _dbcontext.SaveChanges();

            return newPost.Id;
        }
    }
}
