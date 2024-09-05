import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GlobalSectionsAddComponent } from './global-sections-add.component';

describe('GlobalSectionsAddComponent', () => {
  let component: GlobalSectionsAddComponent;
  let fixture: ComponentFixture<GlobalSectionsAddComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [GlobalSectionsAddComponent]
    });
    fixture = TestBed.createComponent(GlobalSectionsAddComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
