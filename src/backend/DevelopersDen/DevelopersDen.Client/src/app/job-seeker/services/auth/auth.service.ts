import { Injectable } from '@angular/core';
import { ApiService } from '../../../shared/services/http/api.service';
import { Observable } from 'rxjs';
import { environment } from '../../../../environments/environment';
import { LoginResponse } from '../../models/response/login';
import { LoginRequest } from '../../models/request/loginRequest';
import { RegisterRequest } from '../../models/request/registerRequest';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private loginApiUrl = environment.jobSeeker.loginUrl;
  private registerApiUrl = environment.jobSeeker.registerUrl;
  private googleApiUrl = environment.jobSeeker.googleLoginurl;

  constructor(private apiservice: ApiService) { }

  Login(credential: LoginRequest): Observable<LoginResponse>{

    return this.apiservice.postData<LoginResponse, LoginRequest>(this.loginApiUrl, credential);
  }

  GoogleLogin(credential: string): Observable<LoginResponse>{

    return this.apiservice.postData<LoginResponse, string>(this.googleApiUrl, JSON.stringify(credential));
  }
  
  Register(credential: any): Observable<any>{

    return this.apiservice.postData<string, any>(this.registerApiUrl, credential);
  }
}
