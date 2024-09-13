import { Injectable } from '@angular/core';
import { LoginData } from '../models/login-data';
import { Token } from '../models/token';
import { Observable, tap } from 'rxjs';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private apiUrl = environment.apiUrl;
  #token?: Token;
  get token()
  {
    return this.#token?.token;
  }

  constructor(private httpClient: HttpClient) { }

  login(loginData: LoginData) : Observable<Token>
  {
    const loginUrl = `${this.apiUrl}/login`;

    return this.httpClient.post<Token>(loginUrl, loginData)
      .pipe(tap(v => this.#token = v));
  }

  isLoggedIn()
  {
    return this.token !== undefined;
  }

  logout()
  {
    this.#token = undefined;
  }
}
