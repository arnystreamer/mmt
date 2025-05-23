import { Component, OnInit } from '@angular/core';
import { LocalSectionCategoriesService } from '../services/local-section-categories.service';
import { ActivatedRoute } from '@angular/router';
import { SectionCategory } from 'src/app/models/sections/section-category.model';
import { SectionCategoryEdit } from 'src/app/models/sections/section-category-edit.model';
import { ItemIdentity } from 'src/app/models/item-identity';
import { Section } from 'src/app/models/static-data/section.model';

@Component({
    selector: 'mmt-local-section-details',
    templateUrl: './local-section-details.component.html',
    styleUrls: [
        './local-section-details.component.scss',
        '../../list-item-details.scss'
    ],
    standalone: false
})
export class LocalSectionDetailsComponent implements OnInit {

  public section?: Section;
  public categories: SectionCategory[] = [];

  constructor(private route: ActivatedRoute, private localSectionCategoriesService: LocalSectionCategoriesService)
  {

  }

  ngOnInit(): void {
    this.route.data.subscribe(({ item }) => this.section = item);
    this.route.data.subscribe(({ itemsApi }) => this.categories.push(...itemsApi.items));
  }

  createWalletSectionCategory(item: SectionCategoryEdit)
  {
    if (!this.section || !item)
      return;

    this.localSectionCategoriesService.post(this.section.id, item).subscribe({next: v => this.categories.push(v) });
  }

  removeWalletSectionCategory(item: ItemIdentity)
  {
    if (!this.section || !item)
      return;

    this.localSectionCategoriesService.delete(this.section.id, item.id).subscribe({next: v => {
      if (v === false)
        return;

      const index = this.categories.findIndex(i => i.id === item.id);
      this.categories.splice(index, 1);
    }})
  }

}
