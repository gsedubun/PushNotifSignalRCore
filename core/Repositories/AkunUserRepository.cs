using core.Models;
using System.Linq;
using System.Text;

namespace core.Repositories
{
    public class AkunUserRepository : Repository<AkunUser>
    {
        private readonly AppDataContext Db;

        public AkunUserRepository(AppDataContext dataContext) : base(dataContext)
        {
            this.Db = dataContext;
        }

        
    }
}
