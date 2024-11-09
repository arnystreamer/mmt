import { Component, OnInit } from '@angular/core';
import { LocalSectionCategoriesService } from '../services/local-section-categories.service';
import { ActivatedRoute } from '@angular/router';
import { LocalSection } from '../models/local-section.model';
import { SectionCategory } from 'src/app/models/sections/section-category.model';

@Component({
  selector: 'mmt-local-section-details',
  templateUrl: './local-section-details.component.html',
  styleUrls: [
    './local-section-details.component.scss',
    '../../list-item-details.scss'
  ]
})
export class LocalSectionDetailsComponent implements OnInit {

  public section?: LocalSection;
  public categories: SectionCategory[] = [];

  constructor(private route: ActivatedRoute, private localSectionCategoriesService: LocalSectionCategoriesService)
  {

  }

  ngOnInit(): void {
    this.route.data.subscribe(({ item }) => this.section = item);
    this.route.data.subscribe(({ itemsApi }) => this.categories.push(...itemsApi.items));
  }

  createWalletSectionCategory(item: SectionCategory)
  {
    if (!this.section || !item)
      return;

    this.localSectionCategoriesService.post(this.section.id, item).subscribe({next: v => this.categories.push(v) });
  }

  removeWalletSectionCategory(item: SectionCategory)
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
