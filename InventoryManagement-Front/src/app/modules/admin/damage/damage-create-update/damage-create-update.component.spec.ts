import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DamageCreateUpdateComponent } from './damage-create-update.component';

describe('DamageCreateUpdateComponent', () => {
  let component: DamageCreateUpdateComponent;
  let fixture: ComponentFixture<DamageCreateUpdateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DamageCreateUpdateComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DamageCreateUpdateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
