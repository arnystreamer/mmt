import { Component, OnInit } from '@angular/core';
import { GlobalSectionsService } from '../services/global-sections.service';
import { ItemWithDescription } from 'src/app/models/item-with-description';
import { GlobalSectionsAddComponent } from './global-sections-add/global-sections-add.component';
import { MatDialog } from '@angular/material/dialog';
import { Observable } from 'rxjs';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-global-sections',
  templateUrl: './global-sections.component.html',
  styleUrls: ['./global-sections.component.scss']
})
export class GlobalSectionsComponent implements OnInit {

  public items: ItemWithDescription[] = [];

  constructor(private route: ActivatedRoute, private globalSectionsService : GlobalSectionsService, private dialog: MatDialog ) {

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
        creator: (item: ItemWithDescription) => this.globalSectionsService.post(item),
        name: '' }
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log(result);

      if (result)
      {
        this.items.push(result);
      }
    });
  }

  editSection(section: ItemWithDescription)
  {
    this.globalSectionsService.put(section).subscribe(i => {
      const index = this.items.findIndex(ix => ix.id == i.id);
      this.items[index].name = section.name;
      this.items[index].description = section.description;
    })
  }

  removeSection(section: ItemWithDescription)
  {
    this.globalSectionsService.delete(section.id).subscribe(i => {
      if (i === false)
        return;

      const index = this.items.findIndex(ix => ix.id == section.id);
      this.items.splice(index, 1);
    })
  }
}
