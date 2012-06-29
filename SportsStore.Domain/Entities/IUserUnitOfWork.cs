using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Store.Domain.Entities
{
    public interface IUserUnitOfWork
    {
        void RegisterCommit(User user);
    }
}
