import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { GlobalSectionsService } from '../../services/global-sections.service';
import { ItemWithDescription } from 'src/app/models/item-with-description';

@Component({
  selector: 'mmt-global-section-details',
  templateUrl: './global-section-details.component.html',
  styleUrls: ['./global-section-details.component.scss']
})
export class GlobalSectionDetailsComponent implements OnInit {

  public section?: ItemWithDescription;

  constructor(private route: ActivatedRoute)
  {

  }

  ngOnInit(): void {
     this.route.data.subscribe(({item}) => this.section = item);
  }
}
