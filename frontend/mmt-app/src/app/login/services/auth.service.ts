import { Injectable } from '@angular/core';
import { LoginData } from '../models/login-data';
import { Token } from '../models/token';
import { Observable, of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private token?: Token;

  constructor() { }

  login(loginData: LoginData) : Observable<Token>
  {
    this.token = {access_token: 'abc'};
    return of(this.token);
  }

  isLoggedIn()
  {
    return this.token !== undefined;
  }

  logout()
  {
    this.token = undefined;
  }
}
