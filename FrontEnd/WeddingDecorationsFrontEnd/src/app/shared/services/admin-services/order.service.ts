import { Injectable } from '@angular/core';
import { Observable, BehaviorSubject, Subject, of } from 'rxjs';
import { environment } from 'src/environments/environment';
import { map, tap, debounceTime, switchMap, delay, catchError } from 'rxjs/operators';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';

import { Order } from '../../models/order';


//Search result - used to keep a list of current displayed orders and total number of orders.
interface SearchResult {
  orders: Order[];
  collectionSize: number;
}

//Used to displayed the current page and size selected.

interface State {
  page: number;
  pageSize: number;
}
@Injectable({
  providedIn: 'root'
})
export class OrderService {
  httpOptions = {headers: new HttpHeaders({'Content-Type': 'application/json'})};
  //Loading - used for loading in files.
  private _loading$ = new BehaviorSubject<boolean>(true);

  //Collectiozine - current collection size
  private _collectionSize$ = new BehaviorSubject<number>(0);

  //Used for searching interface
  private _search$ = new Subject<void>();

  //All orders
  private _orders$ = new BehaviorSubject<Order[]>([]);


  //AllOrderss keeps the array list of orders
  allorderss : Order [];

  //Used to store the current total amount
  totalOrders : number;

 // Used for default data for filtering. SO when page starts you get currently selected orders
  private _state: State = {
    page: 1,
    pageSize: 2
  };


  constructor(private http: HttpClient) {

    //Initial call for to get current orders
    this.getAllOrders(this.page,this.pageSize).subscribe();


    //Used to make search a constant call. So later updates are instantly called.
    this._search$.pipe(
      tap(() => this._loading$.next(true)),
      debounceTime(200),
      switchMap(() => this._search()),
      delay(200),
      tap(() => this._loading$.next(false))
    ).subscribe(result => {
      this._orders$.next(result.orders);
      this._collectionSize$.next(result.collectionSize);
    });

    //Call to initialise search method
    this._search$.next();
  }

  //Getters for main-page HTML.
  get page() { return this._state.page; }
  get pageSize() { return this._state.pageSize; }
  get orders$() { return this._orders$.asObservable(); }
  get collectionSize$() { return this._collectionSize$.asObservable(); }

  //Setters for main-page HTML
  set page(page: number) { this._set({page}); }
  set pageSize(pageSize: number) { this._set({pageSize}); }


  //On set execute line of code
  private _set(patch: Partial<State>) {
    Object.assign(this._state, patch);
    this.getAllOrders(this._state.page,this._state.pageSize).subscribe();
  }


  getAllOrders(page: number, pageSize: number): Observable<any> {
    const params = new HttpParams()
      .set('currentPage', page.toString())
      .set('itemsPerPage', pageSize.toString());
    return this.http.get<any>(environment.apiUrl+"/Order", {params: params}).pipe(map(response => {
      this.allorderss = response.orders;
      this.totalOrders = response.totalCount;
      this._search$.next();
      })
    );
  }

  private _search(): Observable<SearchResult> {
    const {pageSize, page} = this._state;

    let orders =this.allorderss;
    const collectionSize = this.totalOrders;

    return of({orders, collectionSize});
  }

  deleteOrder(orderID: number): Observable<boolean>  {
    const url = `${environment.apiUrl}/Order/${orderID}`;
    return this.http.delete<any>(url)
      .pipe(map(response => {
        // login successful if there's a jwt token in the response
        if (response) {
          this.getAllOrders(this.page,this.pageSize).subscribe();
          return true;
        } else {
          // return false to indicate failed login
          return false;
        }
      })
      );
  }

}
