﻿using DevelopersDen.Contracts.DBModels.Recruiter;
using DevelopersDen.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace DevelopersDen.DataAccess.Repositories
{
    public class RecruiterAccountRepository : GenericRepository<RecruiterAccount>, IRecruiterAccountRepository
    {
        public RecruiterAccountRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<RecruiterAccount?> GetRecruiterAccount(string emailId, string password)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.EmailId == emailId && x.Password == password);
        }
    }
}
