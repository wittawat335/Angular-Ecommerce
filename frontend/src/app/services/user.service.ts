import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment.development';
import { Login } from '../interfaces/login';
import { Observable } from 'rxjs';
import { ResponseApi } from '../interfaces/response-api';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  private baseUrlApi: string = environment.baseUrlApi + 'User';

  constructor(private http: HttpClient) {}

  Login(req: Login): Observable<ResponseApi> {
    return this.http.post<ResponseApi>(`${this.baseUrlApi}/Login`, req);
  }
}
