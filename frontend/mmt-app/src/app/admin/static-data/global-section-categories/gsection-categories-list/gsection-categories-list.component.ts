import { Component, Input, OnInit } from '@angular/core';
import { GlobalCategoriesService } from '../../services/global-categories.service';
import { ActivatedRoute } from '@angular/router';
import { GlobalSectionCategory } from '../../models/global-section-category.model';

@Component({
  selector: 'mmt-gsection-categories-list',
  templateUrl: './gsection-categories-list.component.html',
  styleUrls: ['./gsection-categories-list.component.scss']
})
export class GsectionCategoriesListComponent implements OnInit {
  @Input() sectionId?: number;

  public items: GlobalSectionCategory[] = [];

  constructor(private route: ActivatedRoute,
    private globalCategoriesService: GlobalCategoriesService)
  {

  }

  ngOnInit(): void {
    this.route.data.subscribe(({ itemsApi }) => this.items.push(...itemsApi.items));
  }

  create(item: GlobalSectionCategory)
  {
    if (item && this.sectionId)
    {
      this.globalCategoriesService.post(this.sectionId, item)
        .subscribe({
          next: v => this.items.push(v)
        });
    }
  }

  remove(category: GlobalSectionCategory)
  {
    if (category && this.sectionId)
    {
      this.globalCategoriesService.delete(this.sectionId, category.id)
        .subscribe({
          next: v =>
          {
            if (!v)
              return;

            const index = this.items.findIndex(ix => ix.id == category.id);
            this.items.splice(index, 1);
          }
        });
    }
  }
}
