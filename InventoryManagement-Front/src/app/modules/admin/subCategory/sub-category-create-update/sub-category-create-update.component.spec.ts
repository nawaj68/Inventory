import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SubCategoryCreateUpdateComponent } from './sub-category-create-update.component';

describe('SubCategoryCreateUpdateComponent', () => {
  let component: SubCategoryCreateUpdateComponent;
  let fixture: ComponentFixture<SubCategoryCreateUpdateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SubCategoryCreateUpdateComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SubCategoryCreateUpdateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
