import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Registr } from './registr';

describe('Registr', () => {
  let component: Registr;
  let fixture: ComponentFixture<Registr>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Registr]
    })
    .compileComponents();

    fixture = TestBed.createComponent(Registr);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
