import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CountryCreateUpdateComponent } from './country-create-update.component';

describe('CountryCreateUpdateComponent', () => {
  let component: CountryCreateUpdateComponent;
  let fixture: ComponentFixture<CountryCreateUpdateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CountryCreateUpdateComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CountryCreateUpdateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
