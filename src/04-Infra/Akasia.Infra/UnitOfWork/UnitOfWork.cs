using Akasia.Application;
using Akasia.Application.Repository;
using Akasia.Infra.Respository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Akasia.Infra.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseSession _dbSession;

        public UnitOfWork(DatabaseSession dbSession)
        {
            _dbSession = dbSession;
        }

        public IBlogPostRepository BlogPost => new BlogPostRepository(_dbSession);

        public void Dispose()
        {
            _dbSession.Transaction?.Dispose();
        }

        public void CreateTransaction()
        {
            _dbSession.Transaction = _dbSession.Connection.BeginTransaction();
        }

        public void Commit()
        {
            if (_dbSession.Transaction != null)
                _dbSession.Transaction.Commit();
            Dispose();
        }

        public void Rollback()
        {
            if (_dbSession.Transaction != null)
                _dbSession.Transaction.Rollback();
            Dispose();
        }
    }
}
