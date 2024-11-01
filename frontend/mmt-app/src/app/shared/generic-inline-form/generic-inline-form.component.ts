import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ItemWithDescription } from 'src/app/models/item-with-description';

@Component({
  selector: 'mmt-generic-inline-form',
  templateUrl: './generic-inline-form.component.html',
  styleUrls: ['./generic-inline-form.component.scss',
    '../../list-item-details.scss',
    '../../forms.scss']
})
export class GenericInlineFormComponent {
  @Input() entityName: string = '';
  @Output() createEvent = new EventEmitter<ItemWithDescription>();

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
