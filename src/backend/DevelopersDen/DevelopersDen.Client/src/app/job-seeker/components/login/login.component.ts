import { AfterViewInit, Component, NgZone, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { NavigationEnd, Router } from '@angular/router';
import { CredentialResponse, PromptMomentNotification } from 'google-one-tap';
import { AuthService } from '../../services/auth/auth.service';
import { filter, last, Subscription } from 'rxjs';
import { LoginRequest } from '../../models/request/loginRequest';
import { SessionService } from '../../services/session/session.service';
import { environment } from '../../../../environments/environment';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent implements OnInit, OnDestroy, AfterViewInit {
  public loginValid = true;
  public loginRequest: LoginRequest = { email : "", password: "" };
  private routerSubscription!: Subscription;
  // private _destroySub$ = new Subject<void>();
  // private readonly returnUrl: string;

  constructor(private fb :FormBuilder,
    private _router: Router,
    private service: AuthService,
    private _ngZone: NgZone,
    private sessionService: SessionService
  ) {
    // this.returnUrl = this._route.snapshot.queryParams['returnUrl'] || '/game';
  }
  ngAfterViewInit(): void {
    this.initialiseGoogleOneTap();
  }

  public ngOnInit(): void {
    this.initialiseGoogleOneTap();
    // Subscribe to router events to re-initialize the Google button on navigation
    this.routerSubscription = this._router.events
      .pipe(filter(event => event instanceof NavigationEnd))
      .subscribe(() => {
        // Initialize Google button on navigation end
        this.initialiseGoogleOneTap();
      });
  }

  initialiseGoogleOneTap(){
    // @ts-ignore
    window.onGoogleLibraryLoad = () => {
      console.log('Google\'s One-tap sign in script loaded!');

      // @ts-ignore
      google.accounts.id.initialize({
        // Ref: https://developers.google.com/identity/gsi/web/reference/js-reference#IdConfiguration
        client_id: environment.jobSeeker.googleClientId,
        callback: this.handleCredentialResponse.bind(this), // Whatever function you want to trigger...
        auto_select: false,
        cancel_on_tap_outside: true
      });

      // @ts-ignore
      google.accounts.id.renderButton(
        // @ts-ignore
        document.getElementById("googleBtn"),
          { theme: "outline", size: "large", width: "100%"}
      );
      
      // @ts-ignore
      google.accounts.id.prompt((notification: PromptMomentNotification) => {})
    };
  }

  async handleCredentialResponse(response: CredentialResponse){
    console.log(response);
    await this.service.GoogleLogin(response.credential).subscribe(x => {
      console.log(x);
      this.sessionService.saveDetails(x);
      this._router.navigate(["/jobSeeker/profile"]);
    });
  }

  public ngOnDestroy(): void {
    // this._destroySub$.next();
    if (this.routerSubscription) {
      this.routerSubscription.unsubscribe();
    }
  }

  public onSubmit(): void {
    this.loginValid = true;
    this.service.Login(this.loginRequest).subscribe(res => {
      if(res != null){
        this._ngZone.run(() => {
          this.sessionService.saveDetails(res);
          this._router.navigate(["/jobSeeker/profile"])
        })
      }
    });
  }
}