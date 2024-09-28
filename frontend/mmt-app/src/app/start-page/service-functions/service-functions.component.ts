import { Component } from '@angular/core';
import { HealthCheckService } from 'src/app/services/health-check.service';

@Component({
  selector: 'mmt-service-functions',
  templateUrl: './service-functions.component.html',
  styleUrls: ['./service-functions.component.scss']
})
export class ServiceFunctionsComponent {
  isLoading: boolean = false;
  result: string | undefined = undefined;
  constructor(private service: HealthCheckService) 
  {

  }
  buttonClick() 
  {
    this.isLoading = true;
    this.service.do().subscribe({
      next: v => {
        this.result = v;
        this.isLoading = false;
      }
    });
  }
}
