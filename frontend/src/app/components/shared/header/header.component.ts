import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenService } from 'src/app/services/authen.service';
import { environment } from 'src/environments/environment.development';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css'],
})
export class HeaderComponent {
  constructor(private router: Router) {}

  onLogout() {
    localStorage.removeItem(environment.keyLocalAuth);
    localStorage.removeItem('user');
    this.router.navigate(['/login']);
  }
}
