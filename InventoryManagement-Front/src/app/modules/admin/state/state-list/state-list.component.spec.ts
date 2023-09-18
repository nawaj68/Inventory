import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StateListComponent } from './state-list.component';

describe('StateListComponent', () => {
  let component: StateListComponent;
  let fixture: ComponentFixture<StateListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ StateListComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(StateListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
