import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { GlobalSectionCategory } from '../../models/global-section-category.model';

@Component({
  selector: 'mmt-gsection-categories-add',
  templateUrl: './gsection-categories-add.component.html',
  styleUrls: [
    './gsection-categories-add.component.scss',
    '../../../../list-item-details.scss',
    '../../../../forms.scss'
  ]
})
export class GsectionCategoriesAddComponent implements OnInit {

  @Output() createEvent = new EventEmitter<GlobalSectionCategory>();

  public form!: FormGroup;
  public isFormView: boolean = false;

  constructor(private fromBuilder: FormBuilder)
  {

  }

  ngOnInit(): void {
    this.form = this.fromBuilder.group({
      name: ['', Validators.required],
      description: ['']
    });
  }

  createClick()
  {
    if (this.form.valid)
    {
      this.createEvent.emit({...this.form.value});
    }
  }

  addCategoryClick()
  {
    this.isFormView = true;
  }

  cancelClick()
  {
    this.isFormView = false;
  }
}
