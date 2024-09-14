import { ComponentFixture, TestBed } from '@angular/core/testing';
import { RouterTestingModule } from "@angular/router/testing";
import { MatIconModule } from '@angular/material/icon';

import { GlobalSectionDetailsComponent } from './global-section-details.component';

describe('GlobalSectionDetailsComponent', () => {
  let component: GlobalSectionDetailsComponent;
  let fixture: ComponentFixture<GlobalSectionDetailsComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [GlobalSectionDetailsComponent],
      imports: [
        RouterTestingModule,
        MatIconModule
      ]
    });
    fixture = TestBed.createComponent(GlobalSectionDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
