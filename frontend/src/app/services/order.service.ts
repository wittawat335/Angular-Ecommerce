import { Injectable } from '@angular/core';
import { Order } from '../interfaces/order';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment.development';
import { ResponseApi } from '../interfaces/response-api';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class OrderService {
  private baseUrlApi: string = environment.baseUrlApi + 'Orders';
  constructor(private http: HttpClient) {}

  orderNow(req: Order): Observable<ResponseApi> {
    return this.http.post<ResponseApi>(`${this.baseUrlApi}`, req);
  }

  orderList(): Observable<ResponseApi> {
    let userStore = localStorage.getItem('user');
    let userData = userStore && JSON.parse(userStore);
    return this.http.get<ResponseApi>(`${this.baseUrlApi}/${userData.id}`);
  }

  cancelOrder(orderId: number): Observable<ResponseApi> {
    return this.http.delete<ResponseApi>(`${this.baseUrlApi}/${orderId}`);
  }
}
