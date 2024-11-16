import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { GlobalSectionCreator, GlobalSectionDialogData } from '../models/global-section-dialog-data';
import { SectionEdit } from 'src/app/models/static-data/section-edit.model';

@Component({
  selector: 'mmt-global-sections-add',
  templateUrl: './global-sections-add.component.html',
  styleUrls:
   ['./global-sections-add.component.scss',
    '../../../forms.scss']
})
export class GlobalSectionsAddComponent implements OnInit {

  public form!: FormGroup;
  public errorMessage: string | undefined = undefined;
  private creator: GlobalSectionCreator | undefined = undefined;

  constructor(
    public dialogRef: MatDialogRef<GlobalSectionsAddComponent>,
    @Inject(MAT_DIALOG_DATA) public data: GlobalSectionDialogData,
    private fromBuilder: FormBuilder) {
  }

  ngOnInit(): void {
    this.form = this.fromBuilder.group({
      name: [this.data.name || '', Validators.required],
      description: [this.data.description || '']
    });

    this.creator = this.data.creator;
  }

  create()
  {
    const itemToCreate: SectionEdit = { ...this.form.value };

    if (this.creator)
    {
      this.creator(itemToCreate).subscribe({
        next: v => this.dialogRef.close(v),
        error: e => console.log(e)
      });
    }
    else
    {
      throw 'creatorFactory is undefined';
    }
  }

  cancel()
  {
    this.dialogRef.close();
  }
}
