import { Component, OnInit } from '@angular/core';
import { LocalSection } from './models/local-section.model';
import { LocalSectionsService } from './services/local-sections.service';
import { ActivatedRoute } from '@angular/router';
import { LocalSectionEdit } from './models/local-section-edit.model';
import { ItemIdentity } from '../models/item-identity';

@Component({
  selector: 'mmt-local-sections',
  templateUrl: './local-sections.component.html',
  styleUrls: ['./local-sections.component.scss']
})
export class LocalSectionsComponent implements OnInit {

  public items: LocalSection[] = [];

  constructor(private route: ActivatedRoute,
    private localSectionService: LocalSectionsService) {

  }

  ngOnInit(): void {
    this.route.data.subscribe(({ itemsApi }) => this.items.push(...itemsApi.items));
  }

  createSection(item: LocalSectionEdit)
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
