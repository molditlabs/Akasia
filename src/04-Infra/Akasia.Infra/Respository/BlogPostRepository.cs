using Akasia.Application.Repository;
using Akasia.Domain.Entity;
using Akasia.Infra.UnitOfWork;
using Dapper;
using System;
using System.Collections.Generic;
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

        public async Task CreateAsync(BlogPost request)
        {

            var createQuery = @$"INSERT INTO BlogPost (Title, Content, PostDate, Status, CreatedDate)
                                OUTPUT Inserted.Id
                                VALUES (@Title, @Content, GETDATE(), 1, GETDATE())
                                ";

            var createQueryParams = new
            {
                Title = request.Title,
                Content = request.Content
            };

            request.Id = await _dbSession.Connection.ExecuteScalarAsync<int>(createQuery, createQueryParams, _dbSession.Transaction);
        }

        public Task DeleteAsync(BlogPost entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsRecordExistAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task ReadAsync()
        {
            throw new NotImplementedException();
        }

        public Task ReadByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(BlogPost entity)
        {
            throw new NotImplementedException();
        }
    }
}
