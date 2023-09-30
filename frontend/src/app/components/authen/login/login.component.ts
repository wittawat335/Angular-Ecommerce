import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Login } from 'src/app/interfaces/login';
import { UserService } from 'src/app/services/user.service';
import { UtilityService } from 'src/app/services/utility.service';
import { environment } from 'src/environments/environment.development';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent implements OnInit {
  loginForm: FormGroup;
  checkPassword: boolean = true;
  showLoading: boolean = false;

  constructor(
    private fb: FormBuilder,
    private router: Router,
    private service: UserService,
    private utService: UtilityService
  ) {
    this.loginForm = this.fb.group({
      username: ['', Validators.required],
      password: ['', Validators.required],
    });
  }

  ngOnInit(): void {
    if (localStorage.getItem(environment.keyLocalAuth) != null)
      this.router.navigateByUrl('/stock');
  }

  LoginUser() {
    this.showLoading = true;

    const req: Login = {
      username: this.loginForm.value.username,
      password: this.loginForm.value.password,
    };

    this.service.Login(req).subscribe({
      next: (data) => {
        if (data.isSuccess) {
          console.log(data);
          this.utService.setSessionUser(data.value);
          this.router.navigateByUrl('/stock');
        } else {
        }
      },
      complete: () => {
        this.showLoading = false;
      },
      error: () => {},
    });
  }
}
