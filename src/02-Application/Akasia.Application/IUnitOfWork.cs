using Akasia.Application.Repository;
using Akasia.Application.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace Akasia.Application
{
    public interface IUnitOfWork : IDisposable
    {
        IBlogPostRepository BlogPost { get; }



        void CreateTransaction();
        void Commit();
        void Rollback();
    }
}
