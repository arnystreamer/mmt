import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Section } from 'src/app/models/static-data/section.model';

@Component({
    selector: 'mmt-global-section-details',
    templateUrl: './global-section-details.component.html',
    styleUrls: [
        './global-section-details.component.scss',
        '../../../list-item-details.scss'
    ],
    standalone: false
})
export class GlobalSectionDetailsComponent implements OnInit {

  public section?: Section;

  constructor(private route: ActivatedRoute)
  {

  }

  ngOnInit(): void {
     this.route.data.subscribe(({item}) => this.section = item);
  }
}
