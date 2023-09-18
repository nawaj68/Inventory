import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UnitCreateUpdateComponent } from './unit-create-update.component';

describe('UnitCreateUpdateComponent', () => {
  let component: UnitCreateUpdateComponent;
  let fixture: ComponentFixture<UnitCreateUpdateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UnitCreateUpdateComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UnitCreateUpdateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
