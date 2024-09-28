import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable, tap } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class HealthCheckService {

  apiUrl = environment.apiUrl;

  constructor(private httpClient: HttpClient) { }

  do(): Observable<string>
  {
    return this.httpClient.get(`${this.apiUrl}/health`).pipe(map(v => JSON.stringify(v)));
  }
}
