using Akasia.Application.DTO;
using Akasia.Application.Repository;
using Akasia.Domain.Entity;
using Akasia.Infra.UnitOfWork;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Akasia.Infra.Respository
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly DatabaseSession _dbSession;
        public BlogPostRepository(DatabaseSession dbSession)
        {
            _dbSession = dbSession;
        }

        public async Task<bool> CheckTitleExistAsync(string title)
        {
            var queryParams = new
            {
                Title = title
            };

            return await _dbSession.Connection.ExecuteScalarAsync<bool>("spCheckTitleExist", queryParams, _dbSession.Transaction, commandType: CommandType.StoredProcedure);
        }

        public async Task CreateAsync(BlogPost request)
        {
            var createQueryParams = new
            {
                Title = request.Title,
                Content = request.Content
            };

            request.Id = await _dbSession.Connection.ExecuteScalarAsync<int>("spCreateBlogPost", createQueryParams, _dbSession.Transaction, commandType: CommandType.StoredProcedure);
        }

        public async Task DeleteAsync(int id)
        {
            var queryParams = new
            {
                Id = id,
            };
            await _dbSession.Connection.ExecuteScalarAsync("spDeleteBlogPost", queryParams, _dbSession.Transaction, commandType: CommandType.StoredProcedure);
        }

        public async Task<bool> IsRecordExistAsync(int id)
        {
            var queryParams = new
            {
                Id = id
            };

            return await _dbSession.Connection.ExecuteScalarAsync<bool>("spIsBlogPostExist", queryParams, _dbSession.Transaction, commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<BlogPost>> ReadAllAsync()
        {
            var result = await _dbSession.Connection.QueryAsync<BlogPost>("spReadAllBlogPost", new { }, _dbSession.Transaction, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<BlogPost> ReadByIdAsync(int id)
        {
            var queryParams = new
            {
                Id = id
            };
            var result = await _dbSession.Connection.QueryFirstOrDefaultAsync<BlogPost>("spReadBlogPostById", queryParams, _dbSession.Transaction, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task UpdateAsync(int id, string title, string content)
        {
            var queryParams = new
            {
                Id = id,
                Title = title,
                Content = content
            };
            await _dbSession.Connection.ExecuteScalarAsync("spUpdateBlogPost", queryParams, _dbSession.Transaction, commandType: CommandType.StoredProcedure);
        }
    }
}
