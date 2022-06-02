import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BedNumberComponent } from './bed-number.component';

describe('BedNumberComponent', () => {
  let component: BedNumberComponent;
  let fixture: ComponentFixture<BedNumberComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BedNumberComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BedNumberComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
