import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PurchasesMasterListComponent } from './purchases-master-list.component';

describe('PurchasesMasterListComponent', () => {
  let component: PurchasesMasterListComponent;
  let fixture: ComponentFixture<PurchasesMasterListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PurchasesMasterListComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PurchasesMasterListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
