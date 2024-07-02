export interface JobSeekerProfileDTO {
    jobSeekerProfileId: string;
    summary: string;
    resume: JobSeekerResumeDTO;
    workExperience: WorkExperienceDTO[];
    keySkills: string[];
}
  
export interface JobSeekerResumeDTO {
    fileName: string;
    contentType: string;
    data: string;
}
  
export interface WorkExperienceDTO {
    companyName: string;
    designation: string;
    workDescription: string;
    startDate: Date;
    endDate?: Date;
    isCurrent: number;
}
  