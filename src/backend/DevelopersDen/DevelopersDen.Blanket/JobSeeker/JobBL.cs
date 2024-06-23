using DevelopersDen.Contracts.DTOs.JobSeeker.Requests;
using DevelopersDen.Contracts.DTOs;

namespace DevelopersDen.Blanket.JobSeeker
{
    public class JobBL
    {
        //This method can be used to suggest jobs and also search based on the filters applied
        public async Task<HTTPResponse> GetJobs(string seekerId, JobSearchFilterDTO jobSearchFilter)
        {
            string message = string.Empty;
            Int32 retVal = -40;

            Object? data = default(Object);

            try
            {
                //check if seeker exists or not

                //update search filter in the seeker profile

                //check if the user has already applied for the mentioned jobs
                
            }
            catch (Exception ex)
            {
                return Library.Generic.APIResponse.ConstructExceptionResponse(-40, ex.Message);
            }

            return Library.Generic.APIResponse.ConstructHTTPResponse(data, retVal, message);
        }

        public async Task<HTTPResponse> UpdateJobApplication(string seekerId, string jobId, string status)
        {
            string message = string.Empty;
            Int32 retVal = -40;

            Object? data = default(Object);

            try
            {
                //check if seeker exists or not

                //check if job exists or not

                //check status - 'Apply' or 'Cancel'
            }
            catch (Exception ex)
            {
                return Library.Generic.APIResponse.ConstructExceptionResponse(-40, ex.Message);
            }

            return Library.Generic.APIResponse.ConstructHTTPResponse(data, retVal, message);
        }

        public async Task<HTTPResponse> GetJobApplicationDetails(string seekerId, string jobApplicationId)
        {
            string message = string.Empty;
            Int32 retVal = -40;

            Object? data = default(Object);

            try
            {
                //check if seeker exists or not

                //check if job application exists or not
            }
            catch (Exception ex)
            {
                return Library.Generic.APIResponse.ConstructExceptionResponse(-40, ex.Message);
            }

            return Library.Generic.APIResponse.ConstructHTTPResponse(data, retVal, message);
        }

        public async Task<HTTPResponse> GetAllJobApplicationDetails(string seekerId)
        {
            string message = string.Empty;
            Int32 retVal = -40;

            Object? data = default(Object);

            try
            {
                //check if seeker exists or not
            }
            catch (Exception ex)
            {
                return Library.Generic.APIResponse.ConstructExceptionResponse(-40, ex.Message);
            }

            return Library.Generic.APIResponse.ConstructHTTPResponse(data, retVal, message);
        }

        public async Task<HTTPResponse> SaveJob(string seekerId, string jobId)
        {
            string message = string.Empty;
            Int32 retVal = -40;

            Object? data = default(Object);

            try
            {
                //check if seeker exists or not

                //check if job exists or not
            }
            catch (Exception ex)
            {
                return Library.Generic.APIResponse.ConstructExceptionResponse(-40, ex.Message);
            }

            return Library.Generic.APIResponse.ConstructHTTPResponse(data, retVal, message);
        }

        public async Task<HTTPResponse> GetAllSavedJob(string seekerId)
        {
            string message = string.Empty;
            Int32 retVal = -40;

            Object? data = default(Object);

            try
            {
                //check if seeker exists or not
            }
            catch (Exception ex)
            {
                return Library.Generic.APIResponse.ConstructExceptionResponse(-40, ex.Message);
            }

            return Library.Generic.APIResponse.ConstructHTTPResponse(data, retVal, message);
        }
    }
}
