using Microsoft.EntityFrameworkCore;
using OPTICIP.DataAccessLayer.Models;
using OPTICIP.Entities.Models;
using OPTICIP.IContractLayer.DataAccessRepository;
using OPTICIP.SeedWork;
using System;
using System.Threading.Tasks;

namespace OPTICIP.DataAccessLayer.DataAccessRepository
{
    public class UsersRepository : IUsersRepository
    {
        private readonly CIPContext _context;

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }

        public UsersRepository(CIPContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public TUsers Add(TUsers User)
        {
            return this._context.Add(User).Entity;
        }

        public TUsers Update(TUsers User)
        {
            this._context.Attach(User);
            this._context.TUsers.Update(User);
            this._context.Entry(User).State = EntityState.Modified;

            return User;
        }

        public async Task<TUsers> GetAsync(Guid Id)
        {
            try
            {
                var User = await this._context.TUsers.FindAsync(Id);
                return User;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
