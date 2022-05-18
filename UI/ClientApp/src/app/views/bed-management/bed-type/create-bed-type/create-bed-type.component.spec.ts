import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateBedTypeComponent } from './create-bed-type.component';

describe('CreateBedTypeComponent', () => {
  let component: CreateBedTypeComponent;
  let fixture: ComponentFixture<CreateBedTypeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CreateBedTypeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateBedTypeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
