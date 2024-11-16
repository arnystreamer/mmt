import { ComponentFixture, TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { MatDialog } from '@angular/material/dialog';
import { MatIconModule } from '@angular/material/icon';

import { GlobalSectionsComponent } from './global-sections.component';
import { GlobalSectionsListComponent } from './global-sections-list/global-sections-list.component';



describe('GlobalSectionsComponent', () => {
  let component: GlobalSectionsComponent;
  let fixture: ComponentFixture<GlobalSectionsComponent>;

  let matDialog: jasmine.SpyObj<MatDialog>;
  function matDialogCreator(): MatDialog
  {
    matDialog = jasmine.createSpyObj('MatDialog', ['open']);
    matDialog.open.and.callThrough();
    return matDialog;
  }

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [
        GlobalSectionsComponent,
        GlobalSectionsListComponent
      ],
      imports: [
        RouterTestingModule,
        HttpClientTestingModule,
        MatIconModule
      ],
      providers: [
        { provide: MatDialog, useFactory: matDialogCreator }
      ]

    });
    fixture = TestBed.createComponent(GlobalSectionsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
