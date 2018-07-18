using core.Models;
using core.ViewModels;
using System.Collections.Generic;
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
        
        public bool ValidateUserLogin(AkunLoginViewModel loginViewModel)
        {
            var data = this.Db.AkunUser.Where(d => d.Email == loginViewModel.Email && d.Password == d.Password);
            if (data.Any())
            {
                return true;
            }
            return false;
        }

        public IEnumerable<AkunUserViewModel> Search(string email)
        {
            var data = this.Db.AkunUser.Where(d => d.Email == email).Select(d => new
               AkunRegisterViewModel
            {
                Email = d.Email,
                FullName = d.FullName,
                PhoneNumber = d.PhoneNumber
            }).ToList();
            return data;
        }
        
    }
}
