import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenService } from 'src/app/services/authen.service';
import { environment } from 'src/environments/environment.development';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css'],
})
export class HeaderComponent {
  constructor(private router: Router) {}

  onLogout() {
    Swal.fire({
      title: 'ข้อความ',
      text: 'คุณต้องการออกจากระบบ?',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'ใช่',
      cancelButtonText: 'ไม่',
    }).then((result) => {
      if (result.isConfirmed) {
        localStorage.removeItem(environment.keyLocalAuth);
        localStorage.removeItem('user');
        this.router.navigate(['/login']);
      }
    });
  }
}
