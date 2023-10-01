import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Register } from 'src/app/interfaces/register';
import { AuthenService } from 'src/app/services/authen.service';
import { UtilityService } from 'src/app/services/utility.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
})
export class RegisterComponent implements OnInit {
  registerForm: FormGroup;
  showLoading: boolean = false;

  constructor(
    private fb: FormBuilder,
    private router: Router,
    private service: AuthenService,
    private utService: UtilityService
  ) {
    this.registerForm = this.fb.group({
      username: ['', Validators.required],
      password: ['', Validators.required],
      fullname: ['', Validators.required],
      email: ['', Validators.email],
    });
  }
  ngOnInit(): void {
    throw new Error('Method not implemented.');
  }
  registerUser() {
    this.showLoading = true;

    const req: Register = {
      username: this.registerForm.value.username,
      password: this.registerForm.value.password,
      fullname: this.registerForm.value.fullname,
      email: this.registerForm.value.email,
    };

    this.service.regsiter(req).subscribe({
      next: (data) => {
        if (data.isSuccess) {
          this.router.navigateByUrl('/login');
        } else {
          Swal.fire(data.message);
        }
      },
      complete: () => {
        this.showLoading = false;
      },
      error: () => {},
    });
  }
}
