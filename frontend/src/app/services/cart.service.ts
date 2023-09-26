import { EventEmitter, Injectable } from '@angular/core';
import { Product } from '../interfaces/product';
import { HttpClient } from '@angular/common/http';
import { Cart } from '../interfaces/cart';
import { environment } from 'src/environments/environment.development';
import { ResponseApi } from '../interfaces/response-api';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class CartService {
  private baseUrlApi: string = environment.baseUrlApi + 'Cart';
  cartData = new EventEmitter<Product[] | []>();
  constructor(private http: HttpClient) {}

  localAddToCart(data: Product) {
    let cartData = [];
    let localCart = localStorage.getItem('localCart');
    if (!localCart) {
      localStorage.setItem('localCart', JSON.stringify([data]));
      this.cartData.emit([data]);
    } else {
      cartData = JSON.parse(localCart);
      cartData.push(data);
      localStorage.setItem('localCart', JSON.stringify(cartData));
      this.cartData.emit(cartData);
    }
  }

  removeItemFromCart(productId: number) {
    let cartData = localStorage.getItem('localCart');
    if (cartData) {
      let items: Product[] = JSON.parse(cartData);
      items = items.filter((item: Product) => productId !== item.id);
      localStorage.setItem('localCart', JSON.stringify(items));
      this.cartData.emit(items);
    }
  }

  addToCart(req: Cart): Observable<ResponseApi> {
    return this.http.post<ResponseApi>(`${this.baseUrlApi}/Add`, req);
  }

  getCartList(userId: number): Observable<ResponseApi> {
    return this.http.get<ResponseApi>(`${this.baseUrlApi}/${userId}`);
  }

  removeToCart(cartId: number): Observable<ResponseApi> {
    return this.http.delete<ResponseApi>(`${this.baseUrlApi}/${cartId}`);
  }

  currentCart(): Observable<ResponseApi> {
    let userStore = localStorage.getItem('user');
    let userData = userStore && JSON.parse(userStore);
    return this.http.get<ResponseApi>(
      'http://localhost:3000/cart?userId=' + userData.id
    );
  }

  deleteCartItems(cartId: number): Observable<ResponseApi> {
    return this.http.delete<ResponseApi>(`${this.baseUrlApi}/${cartId}`);
  }
}
