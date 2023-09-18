import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ItemCreateUpdateComponent } from './item-create-update.component';

describe('ItemCreateUpdateComponent', () => {
  let component: ItemCreateUpdateComponent;
  let fixture: ComponentFixture<ItemCreateUpdateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ItemCreateUpdateComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ItemCreateUpdateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
