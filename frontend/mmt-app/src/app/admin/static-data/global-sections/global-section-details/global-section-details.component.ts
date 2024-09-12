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

  public sectionId: string | null = null;
  public section?: ItemWithDescription;

  constructor(private route: ActivatedRoute, private globalSectionsService : GlobalSectionsService)
  {

  }

  ngOnInit(): void {
     this.sectionId = this.route.snapshot.paramMap.get('id');

     if (this.sectionId)
     {
      this.globalSectionsService.get(parseInt(this.sectionId))
        .subscribe({
          next: v => this.section = v
        });
     }
  }
}
