import { Component } from '@angular/core';
import { AuthenService } from './services/authen.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent {
  title = 'frontend';

  constructor(public service: AuthenService) {}
}
