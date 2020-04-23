using Capstone.Abstract;
using Capstone.Contracts;
using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Data
{
    public class UserMessagingRepo : RepositoryBase<UserMessage>, IUserMessaging
    {
        public UserMessagingRepo(ApplicationDbContext applicationDbContext) : base(applicationDbContext) { }
    }
}
