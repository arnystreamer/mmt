import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GlobalSectionsComponent } from './global-sections.component';

describe('GlobalSectionsComponent', () => {
  let component: GlobalSectionsComponent;
  let fixture: ComponentFixture<GlobalSectionsComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [GlobalSectionsComponent]
    });
    fixture = TestBed.createComponent(GlobalSectionsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
