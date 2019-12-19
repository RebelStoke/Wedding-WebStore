import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable, of} from 'rxjs';
import {catchError} from 'rxjs/operators';
import {Order} from './order';

@Injectable({
  providedIn: 'root'
})
export class BookingService {
  private monthly = 'https://localhost:5001/api/order/monthly';
  private orderUrl = 'https://localhost:5001/api/order/';

  constructor(private http: HttpClient) {
  }

  getDatesOfMonth(month): Observable<Order[]> {
    return this.http.post<Order[]>(this.monthly, {year: month.year, month: month.month})
      .pipe(
        catchError(this.handleError<Order[]>('getDatesOfMonth', []))
      );
  }
  createOrder(order): Observable<Order> {
    const s = this.http.post<Order>(this.orderUrl, order);
    location.reload();
    return s;
  }
  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      console.error(error);
      return of(result as T);
    };
  }
}
