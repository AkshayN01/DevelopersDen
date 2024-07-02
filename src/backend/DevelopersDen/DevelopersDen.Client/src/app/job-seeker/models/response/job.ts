export interface JobDTO{
    jobs: Job[],
    totalCount: number
}

export interface Job{
    jobId: string,
    jobTitle: string,
    jobDescription: string,
    minExperience: number,
    keySkills: string[],
    location: string,
    city: string,
    jobType: number,
    postedDate: Date,
    recruiter: Recruiter,
    hasApplied: boolean
}

export interface Recruiter{
    companyName: string,
    companyDescription: string,
    websiteUrl: string
}