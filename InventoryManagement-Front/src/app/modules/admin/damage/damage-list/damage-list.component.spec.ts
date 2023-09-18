import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DamageListComponent } from './damage-list.component';

describe('DamageListComponent', () => {
  let component: DamageListComponent;
  let fixture: ComponentFixture<DamageListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DamageListComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DamageListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
