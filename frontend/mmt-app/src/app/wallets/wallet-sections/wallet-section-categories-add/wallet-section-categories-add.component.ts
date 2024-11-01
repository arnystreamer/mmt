import { Component, EventEmitter, Output } from '@angular/core';
import { WalletSectionCategory } from '../../wallet-section-category.model';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'mmt-wallet-section-categories-add',
  templateUrl: './wallet-section-categories-add.component.html',
  styleUrls: [
    './wallet-section-categories-add.component.scss',
    '../../../list-item-details.scss',
    '../../../forms.scss']
})
export class WalletSectionCategoriesAddComponent {
  @Output() createEvent = new EventEmitter<WalletSectionCategory>();

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

  submitCategory()
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
