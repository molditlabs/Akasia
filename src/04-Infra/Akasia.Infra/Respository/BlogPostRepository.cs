﻿using Akasia.Application.DTO;
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

        public async Task CreateAsync(BlogPost request)
        {
            var createQueryParams = new
            {
                Title = request.Title,
                Content = request.Content
            };

            request.Id = await _dbSession.Connection.ExecuteScalarAsync<int>("spCreateBlogPost", createQueryParams, _dbSession.Transaction, commandType: CommandType.StoredProcedure);
        }

        public Task DeleteAsync(BlogPost entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsRecordExistAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<BlogPost>> ReadAsync()
        {
            var result = await _dbSession.Connection.QueryAsync<BlogPost>("spReadBlogPost", new { }, _dbSession.Transaction, commandType: CommandType.StoredProcedure);
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
            await _dbSession.Connection.ExecuteScalarAsync<BlogPost>("spUpdateBlogPost", queryParams, _dbSession.Transaction, commandType: CommandType.StoredProcedure);

        }
    }
}
