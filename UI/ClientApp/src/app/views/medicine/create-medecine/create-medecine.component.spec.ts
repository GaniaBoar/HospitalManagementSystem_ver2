import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateMedecineComponent } from './create-medecine.component';

describe('CreateMedecineComponent', () => {
  let component: CreateMedecineComponent;
  let fixture: ComponentFixture<CreateMedecineComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CreateMedecineComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateMedecineComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
