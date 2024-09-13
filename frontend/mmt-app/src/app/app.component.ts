import { Component } from '@angular/core';
import { LoaderService } from './loading-overlay/services/loader.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'mmt-app';

  constructor(public loaderService: LoaderService)
  {

  }
}
