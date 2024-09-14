import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';

import { AuthService } from './auth.service';
import { Token } from '../models/token';

describe('AuthService', () => {
  let service: AuthService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule]
    });
    service = TestBed.inject(AuthService);
    httpMock = TestBed.inject(HttpTestingController);

  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should login', () => {

    const tokenResponse: Token = { token: '123' };

    service.login({ login: '', password: '' }).subscribe(r => expect(r).toEqual(tokenResponse));

    httpMock
    .expectOne(request => request.method === "POST"
      && request.body.login === ''
      && request.body.password === ''
      && new URL(request.url).pathname === '/login')
    .flush(tokenResponse);

    expect(service.isLoggedIn()).toBeTrue();
  });

  it('should logout', () => {
    expect(service.isLoggedIn()).toBeFalse();

    service.login({ login: '', password: '' }).subscribe(r =>
      {
        expect(r.token).toBeDefined();
        expect(service.isLoggedIn()).toBeTrue();

        service.logout();

        expect(service.isLoggedIn()).toBeFalse();
      });
  });
});
