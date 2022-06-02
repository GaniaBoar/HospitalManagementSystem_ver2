import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateBedNumberComponent } from './create-bed-number.component';

describe('CreateBedNumberComponent', () => {
  let component: CreateBedNumberComponent;
  let fixture: ComponentFixture<CreateBedNumberComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CreateBedNumberComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateBedNumberComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
