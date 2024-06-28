using DevelopersDen.Contracts.DBModels;
using DevelopersDen.Contracts.DBModels.Job;
using DevelopersDen.Contracts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevelopersDen.DataAccess.Data.Shared
{
    public static class GenericFactData
    {
        public static List<Stakeholder> GetStakeholders()
        {
            return Enum.GetValues(typeof(StakeholderEnum)).Cast<StakeholderEnum>()
                .Select(x => new Stakeholder() { Id = (int)x, IsActive = 1, Name = x.ToString() }).ToList();
        }

        public static List<ApplicationStatus> GetApplicationStatuses()
        {
            return Enum.GetValues(typeof(ApplicationStatusEnum)).Cast<ApplicationStatusEnum>()
                .Select(x => new ApplicationStatus() {  Id = (int)x, Name = x.ToString() }).ToList();
        }
    }
}
