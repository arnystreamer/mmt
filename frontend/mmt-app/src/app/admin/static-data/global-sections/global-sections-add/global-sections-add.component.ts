import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';

@Component({
  selector: 'mmt-global-sections-add',
  templateUrl: './global-sections-add.component.html',
  styleUrls: ['./global-sections-add.component.scss']
})
export class GlobalSectionsAddComponent implements OnInit {
  public form!: FormGroup;

  constructor(private fromBuilder: FormBuilder) {
  }
  ngOnInit(): void {
    this.form = this.fromBuilder.group({
      name: [''],
      description: ['']
    });
  }


}
