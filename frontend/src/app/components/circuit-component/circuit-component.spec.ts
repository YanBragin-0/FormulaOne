import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CircuitComponent } from './circuit-component';

describe('CircuitComponent', () => {
  let component: CircuitComponent;
  let fixture: ComponentFixture<CircuitComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CircuitComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CircuitComponent);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
