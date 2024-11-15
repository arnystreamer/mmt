import { ComponentFixture, TestBed } from '@angular/core/testing';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';

import { GlobalSectionsAddComponent } from './global-sections-add.component';
import { MatFormFieldModule } from '@angular/material/form-field';
import { ReactiveFormsModule } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { provideAnimations } from '@angular/platform-browser/animations';
import { GlobalSectionDialogData } from '../models/global-section-dialog-data';
import { of } from 'rxjs';

describe('GlobalSectionsAddComponent', () => {
  let component: GlobalSectionsAddComponent;
  let fixture: ComponentFixture<GlobalSectionsAddComponent>;

  let matDialogRef: jasmine.SpyObj<MatDialogRef<GlobalSectionsAddComponent>>;
  function matDialogRefCreator(): MatDialogRef<GlobalSectionsAddComponent>
  {
    matDialogRef = jasmine.createSpyObj('MatDialogRef', ['close']);
    return matDialogRef;
  }

  let creatorSpyFunction: jasmine.Spy;
  function dialogDataCreator(): GlobalSectionDialogData
  {
    creatorSpyFunction = jasmine.createSpy('creator');
    const dialogData: GlobalSectionDialogData = {
      name: '',
      description: '',
      creator: (i) =>
      {
        creatorSpyFunction();
        return of(i);
      }
    };

    return dialogData;
  }

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [GlobalSectionsAddComponent],
      imports: [
        ReactiveFormsModule,
        MatFormFieldModule,
        MatInputModule
      ],
      providers: [
        provideAnimations(),
        {provide: MatDialogRef<GlobalSectionsAddComponent>, useFactory: matDialogRefCreator},
        {provide: MAT_DIALOG_DATA, useFactory: dialogDataCreator},
      ]
    });
    fixture = TestBed.createComponent(GlobalSectionsAddComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should invalidate name', () => {
    component.form.setValue({name: '', description: 'some'});

    expect(component.form.valid).toBeFalse();
  });

  it('should not invalidate description', () => {
    component.form.setValue({name: 'name', description: ''});

    expect(component.form.valid).toBeTrue();
  });

  it('create works', () => {
    component.create();
    expect(creatorSpyFunction).toHaveBeenCalledTimes(1);
    expect(matDialogRef.close).toHaveBeenCalledTimes(1);
  });

  it('cancel works', () => {
    component.cancel();
    expect(creatorSpyFunction).not.toHaveBeenCalled();
    expect(matDialogRef.close).toHaveBeenCalledTimes(1);
  });

});
