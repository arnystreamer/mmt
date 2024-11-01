import { Component, EventEmitter, Output } from '@angular/core';
import { WalletSection } from '../../wallet-section.model';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'mmt-wallet-section-add',
  templateUrl: './wallet-section-add.component.html',
  styleUrls: [
    './wallet-section-add.component.scss',
    '../../../list-item-details.scss',
    '../../../forms.scss'
  ]
})
export class WalletSectionAddComponent {
  @Output() createEvent = new EventEmitter<WalletSection>();

  public form!: FormGroup;
  public isFormView: boolean = false;

  constructor(private fromBuilder: FormBuilder)
  {

  }

  ngOnInit(): void {
    this.form = this.fromBuilder.group({
      id: [0],
      name: ['', Validators.required],
      description: ['']
    });
  }

  submitSection()
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
