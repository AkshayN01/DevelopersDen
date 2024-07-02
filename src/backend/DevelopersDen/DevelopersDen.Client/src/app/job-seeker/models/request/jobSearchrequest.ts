export interface JobSearchRequest{
    companyName: string,
    keySkills: string[],
    location: string,
    jobType: number
}

export interface JobType{
    name: string,
    id: number
}