using DevelopersDen.Contracts.DBModels.Job;
using DevelopersDen.Contracts.DBModels.Recruiter;
using DevelopersDen.Contracts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevelopersDen.DataAccess.Data.Recruiter
{
    public class JobType
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public static class JobData
    {
        private static List<Job> Jobs;
        private static List<KeyValuePair<string, List<string>>> JobDataMap = new List<KeyValuePair<string, List<string>>>()
        {
            new KeyValuePair<string, List<string>>("Java", new List<string>(){ "Java", "SpringBoot", "AWS", "Angular", "Javascript", "Oracle", "SQL" }),
            new KeyValuePair<string, List<string>>(".NET", new List<string>(){ ".NET", "Entity Framework", "AWS", "Angular", "Javascript", "SQL Server", "SQL" }),
            new KeyValuePair<string, List<string>>(".NET Framwework", new List<string>(){ ".NET", "Entity Framework", "AWS", "Angular", "Javascript", "SQL Server", "SQL", "ADO .NET", "Identity Framework" }),
            new KeyValuePair<string, List<string>>("NodeJS", new List<string>(){ "NodeJS", "ExpressJS", "MongoDB", "Vue", "Javascript", "AWS", "SQL", "React"}),
            new KeyValuePair<string, List<string>>("VueJS", new List<string>(){ "NodeJS", "ExpressJS", "Vue", "Javascript", "AWS", "HTML", "CSS", "SCSS"}),
            new KeyValuePair<string, List<string>>("ReactJS", new List<string>(){ "NodeJS", "ExpressJS", "ReactJs", "Javascript", "AWS", "HTML", "CSS", "SCSS"}),
            new KeyValuePair<string, List<string>>("Angular", new List<string>(){ "NodeJS", "ExpressJS", "Angular", "Javascript", "AWS", ".NET", "HTML", "CSS", "SCSS"}),
            new KeyValuePair<string, List<string>>("Python", new List<string>(){ "Python", "ExpressJS", "Angular", "Javascript", "AWS", "Google Cloud Platform", "HTML" }),
            new KeyValuePair<string, List<string>>("GoLang", new List<string>(){ "GoLang", "ExpressJS", "Angular", "Javascript", "AWS", "Google Cloud Platform", "HTML", "Docker", "AnsibleScript", "Redis" }),
            new KeyValuePair<string, List<string>>("Android", new List<string>(){ "Java", "Kotilin", "Javascript", "AWS", "Google Cloud Platform", "HTML", "CSS", "SCSS", "Firebase" }),
            new KeyValuePair<string, List<string>>("IOS", new List<string>(){ "c", "Javascript", "AWS", "Google Cloud Platform", "HTML", "CSS", "SCSS", "Firebase" }),
            new KeyValuePair<string, List<string>>("Ionic Framework", new List<string>(){ "Angular", "Typescript", "Javascript", "AWS", "Google Cloud Platform", "HTML", "CSS", "SCSS", "Firebase" }),
        };

        private static readonly List<String> Cities = new List<string>() { "Dublin", "Cork", "Limerick", "Galway", "Kildare", "Belfast", "Waterford", "Londonderry", "Sligo" };

        private static readonly List<String> JobDesignation = new List<string>() { "Senior", "Associate", "Junior", "Mid Level" };

        private static readonly List<JobType> jobTypes = Enum.GetValues(typeof(JobTypeEnum)).Cast<JobTypeEnum>()
            .Select(x => new JobType() { Id = (int)x, Name = x.ToString() }).ToList();
        public static List<Job> GetJobs()
        {
            if(Jobs == null)
            {
                Jobs = new List<Job>();
                List<RecruiterAccount> recruiterAccounts = RecruiterData.GetRecruiterAccounts();
                foreach(var recruiterAccount in recruiterAccounts)
                {
                    foreach(KeyValuePair<string, List<string>> data in JobDataMap)
                    {
                        var location = GetRandomCity();
                        Job job = new Job()
                        {
                            City = location,
                            CreatedAt = DateTime.UtcNow,
                            CreatedBy = "System",
                            IsActive = 1,
                            JobDescription = "The SharePoint developer should: \r\nHave a strong theoretical base in application development using SharePoint and Power Apps, with ability to apply best practice principles to the subject matter context.\r\nHave extensive experience migrating legacy on premise solutions and manual processes (e.g. Access databases) to SharePoint Online and MS Power Platform using a combination of Canvas apps, Model-driven apps and public facing Power pages.\r\nHave experience bringing SharePoint Online/Power Platform solutions through the full development lifecycle.\r\nHave experience developing proof of concepts to demonstrate the effectiveness of the SharePoint Online/Power Platform.\r\nActs as the senior responsible person with respect to SharePoint development on major client engagements.\r\nHave experience in leading and developing complex ICT solutions within SharePoint Online/Power Platform.\r\nTake responsibility for leading the SharePoint development function within the Digital Transformation Unit, including the ability to coordinate contributions of other specialists to complete a joint project.",
                            JobId = Guid.NewGuid(),
                            JobTitle = $"{GetRandomJobDesignation()} {data.Key} Developer",
                            JobType = GetRandomJobType(),
                            KeySkills = data.Value,
                            Location = location,
                            MinExperience = GetRandomExperience(),
                            PostedDate = DateTime.UtcNow,
                            RecruiterAccountId = recruiterAccount.Id,
                            RecruiterId = recruiterAccount.RecruiterId
                        };
                        Jobs.Add(job);
                    }
                }
            }

            return Jobs;
        }
        private static string GetRandomCity()
        {
            Random random = new Random();
            int randomIndex = random.Next(Cities.Count);
            return Cities[randomIndex];
        }
        private static string GetRandomJobDesignation()
        {
            Random random = new Random();
            int randomIndex = random.Next(JobDesignation.Count);
            return JobDesignation[randomIndex];
        }
        private static int GetRandomJobType()
        {
            Random random = new Random();
            int randomIndex = random.Next(jobTypes.Count);
            return jobTypes[randomIndex].Id;
        }
        private static int GetRandomExperience()
        {
            Random random = new Random();
            int randomIndex = random.Next(15);
            return randomIndex;
        }
    }
}
