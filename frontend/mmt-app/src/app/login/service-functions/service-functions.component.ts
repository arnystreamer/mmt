import { Component } from '@angular/core';
import { HealthCheckService } from 'src/app/services/health-check.service';

@Component({
    selector: 'mmt-service-functions',
    templateUrl: './service-functions.component.html',
    styleUrls: ['./service-functions.component.scss'],
    standalone: false
})
export class ServiceFunctionsComponent {
  isLoading: boolean = false;
  result: any | undefined = undefined;
  constructor(private service: HealthCheckService)
  {

  }
  buttonClick()
  {
    this.isLoading = true;
    this.service.do().subscribe({
      next: v => {
        this.result = JSON.parse(v);
        this.isLoading = false;
      }
    });
  }
}
