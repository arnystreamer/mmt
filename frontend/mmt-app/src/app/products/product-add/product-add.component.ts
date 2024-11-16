import { Component, EventEmitter, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Category } from 'src/app/models/static-data/category.model';
import { ProductEdit } from 'src/app/models/static-data/product-edit.model';
import { Section } from 'src/app/models/static-data/section.model';

@Component({
  selector: 'mmt-product-add',
  templateUrl: './product-add.component.html',
  styleUrls: ['./product-add.component.scss',
    '../../list-item-details.scss',
    '../../forms.scss'
  ]
})
export class ProductAddComponent {
  @Output() createEvent = new EventEmitter<ProductEdit>();

  public form!: FormGroup;
  public isFormView: boolean = false;

  public sections: Section[] = [];
  public categories: Category[] = [];

  constructor(private fromBuilder: FormBuilder
  )
  {

  }

  ngOnInit(): void {

    this.form = this.fromBuilder.group({
      name: ['', Validators.required],
      description: ['']
    });
  }

  submitEntity()
  {
    if (this.form.valid)
    {
      this.createEvent.emit({...this.form.value});
      this.hideForm();
    }
  }

  showForm()
  {
    this.isFormView = true;
  }

  hideForm()
  {
    this.form.reset();
    this.isFormView = false;
  }
}
