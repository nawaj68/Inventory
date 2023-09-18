import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ReturnCreateUpdateComponent } from './return-create-update.component';

describe('ReturnCreateUpdateComponent', () => {
  let component: ReturnCreateUpdateComponent;
  let fixture: ComponentFixture<ReturnCreateUpdateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ReturnCreateUpdateComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ReturnCreateUpdateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
