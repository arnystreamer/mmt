import { Component, EventEmitter, Output } from '@angular/core';
import { Wallet } from '../models/wallet.model';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'mmt-wallets-add',
  templateUrl: './wallets-add.component.html',
  styleUrls: [
    './wallets-add.component.scss',
    '../../list-item-details.scss',
    '../../forms.scss'
  ]
})
export class WalletsAddComponent {
  @Output() createEvent = new EventEmitter<Wallet>();

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

  submitWallet()
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
