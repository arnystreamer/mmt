import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { GlobalSection } from '../../models/global-section.model';

@Component({
  selector: 'mmt-global-section-details',
  templateUrl: './global-section-details.component.html',
  styleUrls: ['./global-section-details.component.scss']
})
export class GlobalSectionDetailsComponent implements OnInit {

  public section?: GlobalSection;

  constructor(private route: ActivatedRoute)
  {

  }

  ngOnInit(): void {
     this.route.data.subscribe(({item}) => this.section = item);
  }
}
