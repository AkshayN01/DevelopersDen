import { Injectable } from '@angular/core';
import { ApiService } from '../../../shared/services/http/api.service';
import { environment } from '../../../../environments/environment';
import { Observable, catchError } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { GenericService } from '../../../shared/services/generic/generic.service';
import { JobSeekerProfileDTO } from '../../models/response/profile';

@Injectable({
  providedIn: 'root'
})
export class ProfileService {

  private apiUrl = environment.jobSeeker.apiUrl;

  constructor(private apiService: ApiService, private http: HttpClient, private genericService: GenericService) { }

  getProfile(): Observable<JobSeekerProfileDTO> {
    const api = this.apiUrl + "get-profile";
    return this.apiService.getData<JobSeekerProfileDTO>(api);
  }

  downloadFile() {
    const api = this.apiUrl + "get-resume";
    return this.http.get(api, { responseType: 'blob' });
  }

  saveProfile(data: FormData): Observable<any> {
    const api = this.apiUrl + "add-profile";
    return this.apiService.postData<any, FormData>(api, data);
  }

  updateProfile(data: FormData): Observable<any> {
    const api = this.apiUrl + "update-profile";
    return this.apiService.putData<any, FormData>(api, data);
  }
}
