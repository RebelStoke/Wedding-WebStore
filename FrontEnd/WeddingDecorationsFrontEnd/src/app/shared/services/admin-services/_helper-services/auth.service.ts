import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from '../../../../../environments/environment';
import * as jwt_decode from 'jwt-decode';

import { User } from '../../../models/user';
@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
  private currentUserSubject: BehaviorSubject<User>;
  public currentUser: Observable<User>;

  constructor(private http: HttpClient) {
    this.currentUserSubject = new BehaviorSubject<User>(JSON.parse(localStorage.getItem('currentUser')));
    this.currentUser = this.currentUserSubject.asObservable();
  }

  public get currentUserValue(): User {
    return this.currentUserSubject.value;
  }

  login(username: string, password: string): Observable<boolean> {
    return this.http.post<any>(environment.apiUrl + '/Login', { username, password })
      .pipe(map(response => {
        const token = response.token;

        const decodedToken = jwt_decode(token);
        // login successful if there's a jwt token in the response
        if (token) {
          this.setUpStorage(decodedToken, response);
          // return true to indicate successful login
          return true;
        } else {
          // return false to indicate failed login
          return false;
        }
      }));
  }

  getToken(): string {
    const currentUser = JSON.parse(localStorage.getItem('currentUser'));
    if (currentUser) {
      return currentUser && currentUser.token;
    } else {
      return null;
    }
  }

  getUsername(): string {
    const currentUser = JSON.parse(localStorage.getItem('currentUser'));
    if (currentUser) {
      return currentUser && currentUser.username;
    } else {
      return null;
    }
  }

  getIsAdmin(): string {
    const currentUser = JSON.parse(localStorage.getItem('currentUser'));
    if (currentUser) {
      return currentUser && currentUser.IsAdmin;
    } else {
      return null;
    }
  }

  logout(): void {
    // remove user from local storage to log user out
    localStorage.removeItem('currentUser');
  }

  setUpStorage(decodedToken, response) : void {
    let currentUser = null;
    if ( 'Administrator' === decodedToken['http://schemas.microsoft.com/ws/2008/06/identity/claims/role']){
      currentUser = JSON.stringify({
        username: decodedToken['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'],
        IsAdmin: true,
        token: response.token,
        refreshToken: response.refreshToken
      });
    }
    this.logout(); // Delete existing user

    localStorage.setItem('currentUser', currentUser);
  }

  refresh(): Observable<boolean>  {
    const JSONcurrentUser = JSON.parse(localStorage.getItem('currentUser'));
    return this.http.put<any>(environment.apiUrl + '/Login', { token: JSONcurrentUser.token,
      refreshToken: JSONcurrentUser.refreshToken })
        .pipe(map(response => {
          const token = response.token;

          const decodedToken = jwt_decode(token);
          // login successful if there's a jwt token in the response
          if (token) {
            this.setUpStorage(decodedToken, response);
            // return true to indicate successful login
            return true;
          } else {
            // return false to indicate failed login
            return false;
          }
        }));
  }
}
