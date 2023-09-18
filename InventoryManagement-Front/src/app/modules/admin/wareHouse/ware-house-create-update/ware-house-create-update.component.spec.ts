import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WareHouseCreateUpdateComponent } from './ware-house-create-update.component';

describe('WareHouseCreateUpdateComponent', () => {
  let component: WareHouseCreateUpdateComponent;
  let fixture: ComponentFixture<WareHouseCreateUpdateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ WareHouseCreateUpdateComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(WareHouseCreateUpdateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
