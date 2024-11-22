import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Category } from 'src/app/models/static-data/category.model';
import { ProductEdit } from 'src/app/models/static-data/product-edit.model';
import { Product } from 'src/app/models/static-data/product.model';
import { Section } from 'src/app/models/static-data/section.model';
import { CategoriesService } from 'src/app/services/categories.service';
import { SectionsService } from 'src/app/services/sections.service';

@Component({
  selector: 'mmt-product-add',
  templateUrl: './product-add.component.html',
  styleUrls: ['./product-add.component.scss',
    '../../list-item-details.scss',
    '../../forms.scss'
  ]
})
export class ProductAddComponent {
  @Input() parent?: Product;
  @Input() sections: Section[] = [];
  @Output() createEvent = new EventEmitter<ProductEdit>();

  public form!: FormGroup;
  public isFormView: boolean = false;

  public categories: Category[] = [];

  constructor(private fromBuilder: FormBuilder,
    private sectionsService: SectionsService,
    private categoriesService: CategoriesService)
  {

  }

  ngOnInit(): void {

    this.form = this.fromBuilder.group({
      name: ['', Validators.required],
      description: [''],
      sectionId: [0, Validators.required],
      categoryId: [undefined],
      parentId: [this.parent?.id || null]
    });

    this.form.controls['categoryId'].disable();
    this.form.controls['sectionId'].valueChanges.subscribe({next: v => {

      if (Number.isInteger(v))
      {
        this.form.controls['categoryId'].disable();
        this.categoriesService.getAll(Number.parseInt(v), 0, 10000).subscribe({ next: v =>
        {
          this.categories = v.items;
          this.form.controls['categoryId'].enable();
        }});
      }
      else
      {
        this.categories = [];
      }
    }})
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
    console.log(this.form.value);
  }

  hideForm()
  {
    this.form.reset();
    this.form.controls['categoryId'].disable();
    this.isFormView = false;
  }
}
