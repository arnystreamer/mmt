import { Component, OnInit } from '@angular/core';
import { LocalSectionsService } from './services/local-sections.service';
import { ActivatedRoute } from '@angular/router';
import { ItemIdentity } from '../models/item-identity';
import { Section } from '../models/static-data/section.model';
import { SectionEdit } from '../models/static-data/section-edit.model';

@Component({
    selector: 'mmt-local-sections',
    templateUrl: './local-sections.component.html',
    styleUrls: ['./local-sections.component.scss'],
    standalone: false
})
export class LocalSectionsComponent implements OnInit {

  public items: Section[] = [];

  constructor(private route: ActivatedRoute,
    private localSectionService: LocalSectionsService) {

  }

  ngOnInit(): void {
    this.route.data.subscribe(({ itemsApi }) => this.items.push(...itemsApi.items));
  }

  createSection(item: SectionEdit)
  {
    this.localSectionService.post(item).subscribe({next: v => this.items.push(v) })
  }

  removeSection(item: ItemIdentity)
  {
    this.localSectionService.delete(item.id).subscribe({next: v => {
      if (v === false)
        return;

      const index = this.items.findIndex(i => i.id === item.id);
      this.items.splice(index, 1);
    }

    })
  }

}
