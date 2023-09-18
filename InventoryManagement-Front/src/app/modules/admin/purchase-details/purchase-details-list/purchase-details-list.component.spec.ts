import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PurchaseDetailsListComponent } from './purchase-details-list.component';

describe('PurchaseDetailsListComponent', () => {
  let component: PurchaseDetailsListComponent;
  let fixture: ComponentFixture<PurchaseDetailsListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PurchaseDetailsListComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PurchaseDetailsListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
