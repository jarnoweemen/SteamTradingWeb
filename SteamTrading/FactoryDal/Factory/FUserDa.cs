using DAL.Context;
using DAL.DataAccess;
using InterfaceDal.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryDal.Factory
{
    public class FUserDa
    {
        private readonly ApplicationDbContext _dbContext;

        public FUserDa(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IUserDal GetUserDa()
        {
            return new UserDa(_dbContext);
        }
    }
}
