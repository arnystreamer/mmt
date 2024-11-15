import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GlobalSectionsListComponent } from './global-sections-list.component';

describe('GlobalSectionsListComponent', () => {
  let component: GlobalSectionsListComponent;
  let fixture: ComponentFixture<GlobalSectionsListComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [GlobalSectionsListComponent]
    });
    fixture = TestBed.createComponent(GlobalSectionsListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
