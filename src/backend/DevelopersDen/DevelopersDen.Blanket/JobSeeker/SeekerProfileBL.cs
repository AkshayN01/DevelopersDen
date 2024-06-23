using DevelopersDen.Contracts.DTOs;
using DevelopersDen.Contracts.DTOs.JobSeeker.Requests;

namespace DevelopersDen.Blanket.JobSeeker
{
    public class SeekerProfileBL
    {
        public async Task<HTTPResponse> Login(LoginRequest loginRequest)
        {
            string message = string.Empty;
            Int32 retVal = -40;

            Object? data = default(Object);

            try
            {
                if (String.IsNullOrEmpty(loginRequest.GoogleId) && String.IsNullOrEmpty(loginRequest.Password))
                    throw new Exception("Password or GoogleId is missing");


            }
            catch (Exception ex)
            {
                return Library.Generic.APIResponse.ConstructExceptionResponse(-40, ex.Message);
            }

            return Library.Generic.APIResponse.ConstructHTTPResponse(data, retVal, message);
        }

        public async Task<HTTPResponse> Register(JobSeekerResgisterRequest resgisterRequest)
        {
            string message = string.Empty;
            Int32 retVal = -40;

            Object? data = default(Object);

            try
            {
                
            }
            catch (Exception ex)
            {
                return Library.Generic.APIResponse.ConstructExceptionResponse(-40, ex.Message);
            }

            return Library.Generic.APIResponse.ConstructHTTPResponse(data, retVal, message);
        }
        public async Task<HTTPResponse> AddProfile(string seekerGuid, SeekerProfileRequest profileRequest)
        {
            string message = string.Empty;
            Int32 retVal = -40;

            Object? data = default(Object);

            try
            {
                //check if the seeker exists or not
            }
            catch (Exception ex)
            {
                return Library.Generic.APIResponse.ConstructExceptionResponse(-40, ex.Message);
            }

            return Library.Generic.APIResponse.ConstructHTTPResponse(data, retVal, message);
        }
        public async Task<HTTPResponse> UpdateProfile(string seekerGuid, SeekerProfileRequest profileRequest)
        {
            string message = string.Empty;
            Int32 retVal = -40;

            Object? data = default(Object);

            try
            {
                //check if the seeker exists or not

                
            }
            catch (Exception ex)
            {
                return Library.Generic.APIResponse.ConstructExceptionResponse(-40, ex.Message);
            }

            return Library.Generic.APIResponse.ConstructHTTPResponse(data, retVal, message);
        }
    }
}
