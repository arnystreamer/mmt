import { Component, OnInit } from '@angular/core';
import { GlobalSectionsService } from './services/global-sections.service';
import { GlobalSectionsAddComponent } from './global-sections-add/global-sections-add.component';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute } from '@angular/router';
import { Section } from 'src/app/models/static-data/section.model';
import { ItemIdentity } from 'src/app/models/item-identity';

@Component({
  selector: 'app-global-sections',
  templateUrl: './global-sections.component.html',
  styleUrls: ['./global-sections.component.scss']
})
export class GlobalSectionsComponent implements OnInit {

  public items: Section[] = [];

  constructor(private route: ActivatedRoute,
    private globalSectionsService : GlobalSectionsService,
    private dialog: MatDialog ) {

  }
  ngOnInit(): void {
    this.route.data.subscribe(({ itemsApi }) => this.items.push(...itemsApi.items));
  }

  openCreateDialog()
  {
    let dialogRef = this.dialog.open(GlobalSectionsAddComponent, {
      height: '400pt',
      width: '600pt',
      data: {
        creator: (item: Section) => this.globalSectionsService.post(item),
        name: '' }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result)
      {
        this.items.push(result);
      }
    });
  }

  removeSection(section: ItemIdentity)
  {
    this.globalSectionsService.delete(section.id).subscribe(i => {
      if (i === false)
        return;

      const index = this.items.findIndex(ix => ix.id == section.id);
      this.items.splice(index, 1);
    })
  }
}
