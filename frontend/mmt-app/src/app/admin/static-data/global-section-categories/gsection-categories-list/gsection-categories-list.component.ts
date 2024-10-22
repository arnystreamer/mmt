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
}
