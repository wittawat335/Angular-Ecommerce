import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment.development';
import { Session } from '../interfaces/session';

@Injectable({
  providedIn: 'root',
})
export class UtilityService {
  constructor() {}

  setSessionUser(session: Session) {
    localStorage.setItem(environment.keyLocalAuth, session.token);
    localStorage.setItem('user', JSON.stringify(session));
  }

  getSessionUser() {
    const data = localStorage.getItem('user');
    const user = JSON.parse(data!);
    return user;
  }

  removeSessionUser() {
    localStorage.removeItem('user');
    localStorage.removeItem(environment.keyLocalAuth);
  }
}
