import { Injectable } from '@angular/core';
import { LoginResponse } from '../../models/response/login';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root'
})
export class SessionService {
  storageName: string = "userDetails";
  authStorageName: string = "authToken";
  loginData!: LoginResponse;

  constructor(private jwtHelper: JwtHelperService) { }

  saveDetails = (details: LoginResponse) => {
    localStorage.setItem(this.storageName, JSON.stringify(details));
    localStorage.setItem(this.authStorageName, details.token);
  }

  retreiveDetails = (): LoginResponse | null => {
    var userInfo = localStorage.getItem(this.storageName);
    if(userInfo != null && userInfo != undefined && userInfo != ''){
      this.loginData = JSON.parse(userInfo);
      return this.loginData;
    }
    else
    {
      return null;
    }
  }

  getToken = (): string | null => {
    return localStorage.getItem(this.authStorageName);
  }

  isTokenExpired(): boolean {
    const token = this.getToken();
    return this.jwtHelper.isTokenExpired(token);
  }

  getTokenExpirationDate(): Date | null {
    const token = this.getToken();
    if(token == null)
      return null;
    return this.jwtHelper.getTokenExpirationDate(token);
  }
  
  deleteDetails = () => {
    localStorage.removeItem(this.storageName);
    localStorage.removeItem(this.authStorageName);
  }
}
