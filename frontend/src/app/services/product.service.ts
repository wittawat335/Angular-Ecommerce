import { EventEmitter, Injectable } from '@angular/core';
import { Product } from '../interfaces/product';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment.development';
import { Observable } from 'rxjs';
import { ResponseApi } from '../interfaces/response-api';

@Injectable({
  providedIn: 'root',
})
export class ProductService {
  private baseUrlApi: string = environment.baseUrlApi + 'Product';
  constructor(private http: HttpClient) {}

  getList(): Observable<ResponseApi> {
    return this.http.get<ResponseApi>(this.baseUrlApi);
  }

  getProduct(id: string): Observable<ResponseApi> {
    return this.http.get<ResponseApi>(`${this.baseUrlApi}/${id}`);
  }

  addProduct(req: Product): Observable<ResponseApi> {
    return this.http.post<ResponseApi>(`${this.baseUrlApi}/Add`, req);
  }

  updateProduct(req: Product): Observable<ResponseApi> {
    return this.http.put<ResponseApi>(`${this.baseUrlApi}/Update`, req);
  }

  deleteProduct(id: number): Observable<ResponseApi> {
    return this.http.delete<ResponseApi>(`${this.baseUrlApi}/${id}`);
  }

  popularProducts(): Observable<ResponseApi> {
    return this.http.get<ResponseApi>(`${this.baseUrlApi}?limit=3`);
  }

  trendyProducts(): Observable<ResponseApi> {
    return this.http.get<ResponseApi>(`${this.baseUrlApi}?limit=8`);
  }

  searchProduct(keyword: string): Observable<ResponseApi> {
    return this.http.get<ResponseApi>(
      `${this.baseUrlApi}/search/name?keyword=${keyword}`
    );
  }
}
