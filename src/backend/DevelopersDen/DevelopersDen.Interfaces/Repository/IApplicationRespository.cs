using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevelopersDen.Interfaces.Repository
{
    public interface IApplicationRespository
    {
        Task CancelApplication(Guid id);
    }
}
