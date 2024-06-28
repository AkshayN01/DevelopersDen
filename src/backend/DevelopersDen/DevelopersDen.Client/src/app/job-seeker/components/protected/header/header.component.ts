import { Component } from '@angular/core';
import { MatMenuPanel } from '@angular/material/menu';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrl: './header.component.css'
})
export class HeaderComponent {
  public isAuthenticated = false;

  logout = () => {

  }
  menu!: MatMenuPanel<any> | null;
}
