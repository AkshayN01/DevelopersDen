using DevelopersDen.Contracts.DBModels;
using DevelopersDen.Interfaces.Repository;

namespace DevelopersDen.Library.Services.Seeker
{
    public class StakeholderService
    {
        private readonly IGenericRepository<Stakeholder> _stakeholderRepository;
        public StakeholderService(IGenericRepository<Stakeholder> stakeholderRepository) 
        {
            _stakeholderRepository = stakeholderRepository;
        }

        public async Task<List<Stakeholder?>> GetAllStakeholerDetails()
        {
            return await _stakeholderRepository.GetAllAsync(false);
        }

        public async Task<Stakeholder?> GetStakeholerDetails(int stakeholderId)
        {
            return await _stakeholderRepository.GetByIdAsync(stakeholderId);
        }
    }
}
