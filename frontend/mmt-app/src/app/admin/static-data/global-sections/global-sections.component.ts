import { Component, OnInit } from '@angular/core';
import { GlobalSectionsService } from '../services/global-sections.service';
import { ItemWithDescription } from 'src/app/models/item-with-description';

@Component({
  selector: 'app-global-sections',
  templateUrl: './global-sections.component.html',
  styleUrls: ['./global-sections.component.scss']
})
export class GlobalSectionsComponent implements OnInit {

  public items: ItemWithDescription[] = [];

  constructor(private globalSectionsService : GlobalSectionsService ) {

  }
  ngOnInit(): void {
    this.globalSectionsService.getAll(null, null).subscribe(c => this.items.push(...c.items));
  }

  addSection(section: ItemWithDescription)
  {
    this.globalSectionsService.post(section).subscribe(i => this.items.push(section));
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
