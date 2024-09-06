import { Component, OnInit } from '@angular/core';
import { GlobalSectionsService } from '../services/global-sections.service';
import { ItemWithDescription } from 'src/app/models/item-with-description';
import { GlobalSectionsAddComponent } from './global-sections-add/global-sections-add.component';
import { MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'app-global-sections',
  templateUrl: './global-sections.component.html',
  styleUrls: ['./global-sections.component.scss']
})
export class GlobalSectionsComponent implements OnInit {

  public items: ItemWithDescription[] = [];
  public dialogResult?: string = undefined;

  constructor(private globalSectionsService : GlobalSectionsService, private dialog: MatDialog ) {

  }
  ngOnInit(): void {
    this.globalSectionsService.getAll(null, null).subscribe(c => this.items.push(...c.items));
  }

  openCreateDialog()
  {
    function getAddSectionFunction(component: GlobalSectionsComponent): (item : ItemWithDescription) => Promise<boolean>
    {
      return (item) => component.addSectionAsync(item);
    }

    let dialogRef = this.dialog.open(GlobalSectionsAddComponent, {
      height: '400pt',
      width: '600pt',
      data: { submitAsync: getAddSectionFunction(this) }
    });

    dialogRef.afterClosed().subscribe(result => this.dialogResult = JSON.stringify(result))
  }

  addSectionAsync(section: ItemWithDescription): Promise<boolean>
  {
    var service = this.globalSectionsService;

    return new Promise((resolve, reject) =>
    {
      service.post(section).subscribe({
        next: v => {
            this.items.push(section);
            return resolve(true);
          },
        error: e => {
          console.log(e);
          return resolve(false);
        }
      });
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
