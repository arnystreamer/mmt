import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GlobalSectionsListItemComponent } from './global-sections-list-item.component';

describe('GlobalSectionsListItemComponent', () => {
  let component: GlobalSectionsListItemComponent;
  let fixture: ComponentFixture<GlobalSectionsListItemComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [GlobalSectionsListItemComponent]
    });
    fixture = TestBed.createComponent(GlobalSectionsListItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
