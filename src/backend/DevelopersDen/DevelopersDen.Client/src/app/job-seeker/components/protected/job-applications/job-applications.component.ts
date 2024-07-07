import { Component } from '@angular/core';
import { PageEvent } from '@angular/material/paginator';
import { Job } from '../../../models/response/job';
import { FormBuilder } from '@angular/forms';
import { JobService } from '../../../services/job/job.service';
import { ApplicationStatuses, JobApplication, JobApplicationDTO } from '../../../models/response/jobApplication';
import { JobType } from '../../../models/request/jobSearchrequest';
import { GenericService } from '../../../../shared/services/generic/generic.service';

@Component({
  selector: 'app-job-applications',
  templateUrl: './job-applications.component.html',
  styleUrl: './job-applications.component.css'
})
export class JobApplicationsComponent {
  pageSize = 10; 
  pageNumber = 1;
  jobs!: JobApplicationDTO;
  jobTypes!: JobType[];
  applicationStatuses!: ApplicationStatuses[];
  hasDataLoaded: boolean = false;
  isSelected: boolean = false;
  selectedJob!: JobApplication;

  constructor(private fb: FormBuilder, private jobService: JobService, private genericService: GenericService)
  {
  }
  ngOnInit(): void {
    this.initialiseData();
  }

  initialiseData(){
    this.jobService.getJobType().subscribe(res => {
      this.jobTypes = res;
    });
    this.jobService.getApplicationStatuses().subscribe(res => {
      this.applicationStatuses = res;
    });
    this.getJobApplications();
  }

  viewInfo(job: JobApplication){
    this.selectedJob = job;
    if(this.selectedJob.applicationStatusId == 2)
      this.selectedJob.hasCanceled = true;
    this.isSelected = true;
  }

  getJobTypeName(jobTypeId: number): string {
    const jobType = this.jobTypes.find(type => type.id === jobTypeId);
    return jobType ? jobType.name : 'Unknown';
  }

  getStatusName(statusId: number): string {
    const status = this.applicationStatuses.find(type => type.id === statusId);
    return status ? status.name : 'Unknown';
  }

  onPageChange(event: PageEvent): void {
    console.log(event.pageIndex);
    this.pageSize = event.pageSize;
    this.pageNumber = event.pageIndex + 1;
    this.getJobApplications();
  }

  applyJob(){
    this.jobService.editjobApplication(this.selectedJob.applicationId, 2).subscribe(res =>{
      if(res == 'true' || res == true){
        this.genericService.openSnackBar('Successfully cancelled application');
        var index = this.jobs.applications.findIndex(x => x.applicationId == this.selectedJob.applicationId);
        this.jobs.applications[index].hasCanceled = true;
      }
    })
  }
  getJobApplications(){
    this.hasDataLoaded = false;
    this.jobService.getAllJobApplications(this.pageNumber, this.pageSize).subscribe(res => {
      console.log(res);
      this.jobs = res;
      this.hasDataLoaded = true;
    });
  }
}
