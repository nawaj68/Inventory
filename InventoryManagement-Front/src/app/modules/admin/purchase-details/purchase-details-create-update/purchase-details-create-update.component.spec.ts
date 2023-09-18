import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PurchaseDetailsCreateUpdateComponent } from './purchase-details-create-update.component';

describe('PurchaseDetailsCreateUpdateComponent', () => {
  let component: PurchaseDetailsCreateUpdateComponent;
  let fixture: ComponentFixture<PurchaseDetailsCreateUpdateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PurchaseDetailsCreateUpdateComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PurchaseDetailsCreateUpdateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
