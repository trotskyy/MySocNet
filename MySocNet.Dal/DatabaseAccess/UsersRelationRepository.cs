using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySocNet.Dal.Abstract;
using MySocNet.Dal.Entities;

namespace MySocNet.Dal
{
    public class UsersRelationRepository : GenericRepository<UsersRelation>, IUsersRelationRepository
    {
        public UsersRelationRepository(MySocNetContext dbContext) : base(dbContext)
        {
        }
    }
}
