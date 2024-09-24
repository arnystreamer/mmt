import { Component } from '@angular/core';

@Component({
  selector: 'mmt-gsection-categories-add',
  templateUrl: './gsection-categories-add.component.html',
  styleUrls: ['./gsection-categories-add.component.scss']
})
export class GsectionCategoriesAddComponent {
  public isFormView: boolean = false;

  addCategoryClick()
  {
    this.isFormView = true;
  }

  cancelClick()
  {
    this.isFormView = false;
  }
}
