import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { GlobalSectionDialogData } from '../../models/global-section-dialog-data';
import { ItemWithDescription } from 'src/app/models/item-with-description';

@Component({
  selector: 'mmt-global-sections-add',
  templateUrl: './global-sections-add.component.html',
  styleUrls: ['./global-sections-add.component.scss']
})
export class GlobalSectionsAddComponent implements OnInit {

  public form!: FormGroup;
  public errorMessage: string | undefined = undefined;

  private createAsync: (item: ItemWithDescription) => Promise<boolean> = (i) => Promise.resolve(true);

  constructor(
    public dialogRef: MatDialogRef<GlobalSectionsAddComponent>,
    @Inject(MAT_DIALOG_DATA) public data: GlobalSectionDialogData,
    private fromBuilder: FormBuilder) {
      this.createAsync = data.submitAsync;
  }

  ngOnInit(): void {
    this.form = this.fromBuilder.group({
      name: [this.data.name || '', Validators.required],
      description: [this.data.description || '']
    });
  }

  create()
  {
    this.createAsync({ ...this.form.value }).then((v) =>
    {
      if (v)
        this.dialogRef.close(this.form.value);
      else
        this.errorMessage = "Creation was unsuccessful"

    },
    (e) =>
    {
      this.errorMessage = `Creation was unsuccessful: ${e}`
    });


  }

  cancel()
  {
    this.dialogRef.close();
  }
}
