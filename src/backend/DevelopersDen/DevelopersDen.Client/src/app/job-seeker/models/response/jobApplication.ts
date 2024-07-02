import { Job } from "./job"

export interface JobApplicationDTO{
    applications: JobApplication[],
    totalCount: number
}

export interface JobApplication{
    applicationId: string,
    comments: string,
    applicationStatusId: number,
    hasCanceled:boolean,
    job: Job
}

export interface ApplicationStatuses{
    id: number,
    name: string
}