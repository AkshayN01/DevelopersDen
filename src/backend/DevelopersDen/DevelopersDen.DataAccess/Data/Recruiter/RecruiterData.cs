using DevelopersDen.Contracts.DBModels.Recruiter;
using DevelopersDen.Contracts.Enums;
using DevelopersDen.Library.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DevelopersDen.DataAccess.Data.Recruiter
{
    public static class RecruiterData
    {
        private static readonly List<string> CompanyNames = new List<string>() { "Tech Mahindra", "TCS", "Deloitte", "EY", "Microsoft", "Amazon", "Tesla", "DFDS", "Sportz Interactive",
            "Swabhav Techlabs", "Red Black Tree", "Bajaj", "Crossover", "Yes Bank", "Morgan Stanley", "JP Morgan", "Google", "Facebook", "AIB", "Revolut" };

        private static List<Contracts.DBModels.Recruiter.Recruiter> Recruiters;
        private static List<RecruiterAccount> RecruiterAccounts;
        public static List<Contracts.DBModels.Recruiter.Recruiter> GetRecruiters()
        {
            if (Recruiters == null)
            {
                Recruiters = new List<Contracts.DBModels.Recruiter.Recruiter>();
                foreach (string companyName in CompanyNames)
                {
                    Contracts.DBModels.Recruiter.Recruiter recruiter = new Contracts.DBModels.Recruiter.Recruiter()
                    {
                        RecruiterId = Guid.NewGuid(),
                        Address = "Dublin 22, Dublin, Ireland",
                        City = "Dublin",
                        CompanyDescription = "We are a Digital Product Engineering company that is scaling in a big way! We build products, services, and experiences that inspire, excite, and delight. We work at scale across all devices and digital mediums, and our people exist everywhere in the world (17000+ experts across 32 countries, to be exact). Our work culture is dynamic and non-hierarchical. We are looking for great new colleagues. That is where you come in!",
                        CompanyName = companyName,
                        CreatedAt = DateTime.UtcNow,
                        CreatedBy = "System",
                        EmailId = GenerateCompanyEmail(companyName),
                        IsActive = 1,
                        StakeholderId = (int)StakeholderEnum.Recruiter,
                        WebsiteUrl = GenerateCompanyWebsiteUrl(companyName),
                    };
                    Recruiters.Add(recruiter);
                }
            }

            return Recruiters;
        }
        private static String GenerateCompanyEmail(string companyName)
        {
            string companynameinoneword = ConvertToOneWord(companyName);
            return companynameinoneword + "info@" + companynameinoneword + ".com";
        }
        private static String GenerateCompanyWebsiteUrl(string companyName)
        {
            string companynameinoneword = ConvertToOneWord(companyName);
            return "www." + companynameinoneword  + ".com";
        }
        private static string ConvertToOneWord(string input)
        {
            if (input == null) throw new ArgumentNullException(nameof(input));
            return Regex.Replace(input, @"\s+", "");
        }

        #region " Recruiter Account "

        public static List<RecruiterAccount> GetRecruiterAccounts()
        {
            if(RecruiterAccounts == null)
            {
                RecruiterAccounts = new List<RecruiterAccount>();
                var recruiters = GetRecruiters();
                foreach (var recruiter in recruiters)
                {
                    RecruiterAccount recruiterAccount = new RecruiterAccount()
                    {
                        CreatedBy = "System",
                        CreatedAt = DateTime.UtcNow,
                        EmailId = recruiter.EmailId.Replace("info", "hr"),
                        Id = Guid.NewGuid(),
                        IsActive = 1,
                        IsPrimary = 1,
                        IsVerified = 1,
                        Password = PasswordHasher.HashPassword("test@" + recruiter.CompanyName),
                        RecruiterId = recruiter.RecruiterId
                    };

                    RecruiterAccounts.Add(recruiterAccount);
                }
            }

            return RecruiterAccounts;
        }

        #endregion
    }
}
