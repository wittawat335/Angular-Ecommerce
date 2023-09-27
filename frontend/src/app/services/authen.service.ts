import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ResponseApi } from '../interfaces/response-api';
import { Login } from '../interfaces/login';
import { Register } from '../interfaces/register';
import { environment } from 'src/environments/environment.development';

@Injectable({
  providedIn: 'root',
})
export class AuthenService {
  private headers = new HttpHeaders({ 'Content-Type': 'application/json' });
  private baseUrlApi: string = environment.baseUrlApi + 'Authen';

  constructor(private http: HttpClient) {}

  login(data: Login): Observable<ResponseApi> {
    return this.http.post<ResponseApi>(`${this.baseUrlApi}/login`, data, {
      headers: this.headers,
    });
  }

  regsiter(data: Register): Observable<ResponseApi> {
    return this.http.post<ResponseApi>(`${this.baseUrlApi}/register`, data, {
      headers: this.headers,
    });
  }

  isLoggedIn(): boolean {
    const authenInfo = JSON.parse(
      localStorage.getItem(environment.keyLocalAuth) || ''
    );
    return authenInfo != null;
  }
}
