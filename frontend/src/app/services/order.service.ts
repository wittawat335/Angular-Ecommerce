import { Injectable } from '@angular/core';
import { Order } from '../interfaces/order';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class OrderService {
  constructor(private http: HttpClient) {}
  orderNow(data: Order) {
    return this.http.post('http://localhost:3000/orders', data);
  }

  orderList() {
    let userStore = localStorage.getItem('user');
    let userData = userStore && JSON.parse(userStore);
    return this.http.get<Order[]>(
      'http://localhost:3000/orders?userId=' + userData.id
    );
  }

  cancelOrder(orderId: number) {
    return this.http.delete('http://localhost:3000/orders/' + orderId);
  }
}
