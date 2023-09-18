import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CityCreateUpdateComponent } from './city-create-update.component';

describe('CityCreateUpdateComponent', () => {
  let component: CityCreateUpdateComponent;
  let fixture: ComponentFixture<CityCreateUpdateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CityCreateUpdateComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CityCreateUpdateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
