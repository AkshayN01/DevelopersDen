import { Injectable } from '@angular/core';

import { HttpClient } from '@angular/common/http';

interface Config {
  apiUrl: string;
  loginAPIUrl: string
  oAuth: oAuthConfig;
}
interface oAuthConfig {
  issuer: string;
  redirectUri: string;
  postLogoutRedirectUri: string;
}

export interface IAppConfig {
  config: Config;
  load: () => Promise<void>;
}

@Injectable()
export class AppConfig implements IAppConfig {
  public config: Config;

  constructor(private readonly http: HttpClient) { 
    this.config = {} as Config;
  }

  public load(): Promise<void> {
    return new Promise((resolve, reject) => {
        this.http.get<Config>('assets/config.json')
        .subscribe(data => {this.config = data; 
          console.log(this.config);resolve();});
    });
  }
}

export function initConfig(config: AppConfig): () => Promise<void> {
  return () => config.load();
}