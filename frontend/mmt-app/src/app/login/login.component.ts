import { Component, OnInit } from '@angular/core';
import { AuthService } from './services/auth.service';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
    selector: 'mmt-login',
    templateUrl: './login.component.html',
    styleUrls: [
        './login.component.scss',
        '../forms.scss'
    ],
    standalone: false
})
export class LoginComponent implements OnInit {

  public form!: FormGroup;
  public errorMessage?: string;

  constructor(private authService: AuthService,
    private router: Router,
    private fromBuilder: FormBuilder)
  {

  }
  ngOnInit(): void {
    this.form = this.fromBuilder.group({
      login: ['', Validators.required],
      password: ['', Validators.required]
    });
  }

  login()
  {
    this.authService.login({...this.form.value})
    .subscribe(
      {
        next: v => this.router.navigate(['/']),
        error: e => this.errorMessage = `Error: ${ e.message ? JSON.stringify(e.message) : e }`
      }
    )
  }
}
