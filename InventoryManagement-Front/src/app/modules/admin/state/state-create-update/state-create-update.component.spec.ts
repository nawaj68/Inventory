import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StateCreateUpdateComponent } from './state-create-update.component';

describe('StateCreateUpdateComponent', () => {
  let component: StateCreateUpdateComponent;
  let fixture: ComponentFixture<StateCreateUpdateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ StateCreateUpdateComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(StateCreateUpdateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
