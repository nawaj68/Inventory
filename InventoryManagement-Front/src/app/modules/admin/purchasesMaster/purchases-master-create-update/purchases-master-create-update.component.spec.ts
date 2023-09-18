import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PurchasesMasterCreateUpdateComponent } from './purchases-master-create-update.component';

describe('PurchasesMasterCreateUpdateComponent', () => {
  let component: PurchasesMasterCreateUpdateComponent;
  let fixture: ComponentFixture<PurchasesMasterCreateUpdateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PurchasesMasterCreateUpdateComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PurchasesMasterCreateUpdateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
