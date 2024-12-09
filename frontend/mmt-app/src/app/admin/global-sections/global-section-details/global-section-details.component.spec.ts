import { ComponentFixture, TestBed } from '@angular/core/testing';
import { RouterTestingModule } from "@angular/router/testing";
import { MatIconModule } from '@angular/material/icon';

import { GlobalSectionDetailsComponent } from './global-section-details.component';
import { GsectionCategoriesListComponent } from '../global-section-categories/gsection-categories-list/gsection-categories-list.component';
import { provideHttpClientTesting } from '@angular/common/http/testing';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';

describe('GlobalSectionDetailsComponent', () => {
  let component: GlobalSectionDetailsComponent;
  let fixture: ComponentFixture<GlobalSectionDetailsComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
    declarations: [
        GlobalSectionDetailsComponent,
        GsectionCategoriesListComponent
    ],
    imports: [RouterTestingModule,
        MatIconModule],
    providers: [provideHttpClient(withInterceptorsFromDi()), provideHttpClientTesting()]
});
    fixture = TestBed.createComponent(GlobalSectionDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
