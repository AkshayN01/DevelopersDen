import { Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { ApiService } from '../../../shared/services/http/api.service';
import { Observable } from 'rxjs';
import { JobDTO } from '../../models/response/job';
import { ApplicationStatuses, JobApplication, JobApplicationDTO } from '../../models/response/jobApplication';
import { JobSearchRequest, JobType } from '../../models/request/jobSearchrequest';

@Injectable({
  providedIn: 'root'
})
export class JobService {

  private apiUrl = environment.jobSeeker.apiUrl;
  private genericUrl = environment.jobSeeker.genericApiUrl;

  constructor(private apiService: ApiService) { }

  getJobType(): Observable<JobType[]> {
    const api = this.genericUrl + "get-job-types";
    return this.apiService.getData<JobType[]>(api);
  }

  getApplicationStatuses(): Observable<ApplicationStatuses[]> {
    const api = this.genericUrl + "get-application-statuses";
    return this.apiService.getData<ApplicationStatuses[]>(api);
  }

  getjobs(body: JobSearchRequest, pageNo: number, pageSize: number): Observable<JobDTO> {
    const api = this.apiUrl + "get-jobs?pageNumber="+pageNo+"&pageSize="+pageSize;
    return this.apiService.postData<JobDTO, JobSearchRequest>(api, body);
  }

  getAllJobApplications(pageNo: number, pageSize: number): Observable<any> {
    const api = this.apiUrl + "get-all-job-applications?pageNumber="+pageNo+"&pageSize="+pageSize;
    return this.apiService.getData<JobApplicationDTO>(api);
  }

  getjobApplication(jobApplicationId: string): Observable<any> {
    const api = this.apiUrl + "get-job-application/" + jobApplicationId;
    return this.apiService.getData<JobApplication>(api);
  }
  
  addjobApplication(jobId: string, status: number): Observable<any> {
    const api = this.apiUrl + "add-job-application/" + jobId + "?status="+status;
    return this.apiService.postData<any, any>(api, null);
  }

  editjobApplication(jobApplicationId: string, status: number): Observable<any> {
    const api = this.apiUrl + "edit-job-application/" + jobApplicationId + "?status="+status;
    return this.apiService.putData<any, any>(api, null);
  }
}
