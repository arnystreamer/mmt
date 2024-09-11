import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { ItemWithDescriptionCreator, GlobalSectionDialogData } from '../../models/global-section-dialog-data';
import { ItemWithDescription } from 'src/app/models/item-with-description';

@Component({
  selector: 'mmt-global-sections-add',
  templateUrl: './global-sections-add.component.html',
  styleUrls: ['./global-sections-add.component.scss']
})
export class GlobalSectionsAddComponent implements OnInit {

  public form!: FormGroup;
  public errorMessage: string | undefined = undefined;
  private creator: ItemWithDescriptionCreator | undefined = undefined;

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
    const itemToCreate: ItemWithDescription = { ...this.form.value };

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
