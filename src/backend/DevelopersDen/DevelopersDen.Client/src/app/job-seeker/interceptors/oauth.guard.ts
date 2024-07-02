import { Injectable, inject } from '@angular/core';
import { ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router, CanActivateFn } from '@angular/router';
import { Observable, of } from 'rxjs';
import { SessionService } from '../services/session/session.service';

@Injectable({
  providedIn: 'root'
})
class OauthGuard {

  constructor(private router: Router, private sesssionStorageService: SessionService) {

  }

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    let canActivate = false;
    if (!this.sesssionStorageService.isTokenExpired()) 
    {
      console.log("OAuth valid");
      canActivate = true;
    } 
    else {
      canActivate = false;
      return this.router.createUrlTree(['/jobSeeker/login'], {queryParams: {returnUrl: state.url}});
    }
    console.debug('canActivate:', canActivate);
    return of(canActivate).pipe();
  }
}

export const isAuthGuard: CanActivateFn = ( route: ActivatedRouteSnapshot, state: RouterStateSnapshot) => 
{
    return inject(OauthGuard).canActivate(route, state);
}