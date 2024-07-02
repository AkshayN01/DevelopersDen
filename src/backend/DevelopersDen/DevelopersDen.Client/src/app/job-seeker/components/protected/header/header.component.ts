import { Component, OnInit } from '@angular/core';
import { MatMenuPanel } from '@angular/material/menu';
import { SessionService } from '../../../services/session/session.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrl: './header.component.css'
})
export class HeaderComponent implements OnInit {
  public isAuthenticated = false;

  constructor(private sessionService: SessionService, private router: Router){}

  ngOnInit(): void {
    var details = this.sessionService.retreiveDetails();
    if(details != null)
      this.isAuthenticated = true;
  }
  logout = () => {
    this.sessionService.deleteDetails();
    this.router.navigate(["/jobSeeker/login"]);
  }
  menu!: MatMenuPanel<any> | null;
}
