using AutoMapper;
using DevelopersDen.Contracts.DTOs;
using DevelopersDen.Interfaces.Repository;
using DevelopersDen.Library.Services.Seeker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevelopersDen.Blanket.Generic
{
    public class GenericBL
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public GenericBL(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<HTTPResponse> GetStakeholders()
        {
            string message = string.Empty;
            Int32 retVal = -40;

            Object? data = default(Object);

            try
            {
                //check if seeker exists or not
                data = await _unitOfWork._StakeholderRepository.GetAllAsync();
                retVal = 1;
            }
            catch (Exception ex)
            {
                return Library.Generic.APIResponse.ConstructExceptionResponse(-40, ex.Message);
            }

            return Library.Generic.APIResponse.ConstructHTTPResponse(data, retVal, message);
        }

        public async Task<HTTPResponse> GetApplicationStatuses()
        {
            string message = string.Empty;
            Int32 retVal = -40;

            Object? data = default(Object);

            try
            {
                //check if seeker exists or not
                data = await _unitOfWork._ApplicationStatusRepository.GetAllAsync();
                retVal = 1;
            }
            catch (Exception ex)
            {
                return Library.Generic.APIResponse.ConstructExceptionResponse(-40, ex.Message);
            }

            return Library.Generic.APIResponse.ConstructHTTPResponse(data, retVal, message);
        }
    }
}
