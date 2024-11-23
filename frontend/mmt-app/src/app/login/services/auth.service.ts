import { Injectable } from '@angular/core';
import { LoginData } from '../models/login-data';
import { Token } from '../models/token';
import { Observable, tap } from 'rxjs';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { TimedToken } from '../models/timed-token';

const storageKey = 'auth_local';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private apiUrl = environment.apiUrl;
  #token?: TimedToken;
  get token()
  {
    if (!this.#token)
      return undefined;

    const expiresAt = this.#token.expiredAtMs;
    const now = Date.now();

    return (expiresAt > now) ? this.#token.token : undefined;
  }

  constructor(private httpClient: HttpClient) {

    const storedTokenString = localStorage.getItem(storageKey);
    if (storedTokenString)
    {
      const storedToken: TimedToken = JSON.parse(storedTokenString);

      if (storedToken)
      {
        if (storedToken.expiredAtMs > Date.now())
          this.#token = storedToken;
        else
          localStorage.removeItem(storageKey);
      }
    }

  }

  login(loginData: LoginData) : Observable<Token>
  {
    const loginUrl = `${this.apiUrl}/login`;

    return this.httpClient.post<Token>(loginUrl, loginData)
      .pipe(tap(v => {
        const tokenObj: TimedToken = { ...v, expiredAtMs: Date.now() + 7200000 };
        this.#token = tokenObj;
        localStorage.setItem(storageKey, JSON.stringify(tokenObj));
      } ));
  }

  isLoggedIn()
  {
    var isLoggedInAndNotExpired = this.token !== undefined;

    if (!isLoggedInAndNotExpired)
      localStorage.removeItem(storageKey);

    return isLoggedInAndNotExpired;
  }

  logout()
  {
    this.#token = undefined;
  }
}
