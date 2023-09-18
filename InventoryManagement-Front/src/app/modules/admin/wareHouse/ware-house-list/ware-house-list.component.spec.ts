import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WareHouseListComponent } from './ware-house-list.component';

describe('WareHouseListComponent', () => {
  let component: WareHouseListComponent;
  let fixture: ComponentFixture<WareHouseListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ WareHouseListComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(WareHouseListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
