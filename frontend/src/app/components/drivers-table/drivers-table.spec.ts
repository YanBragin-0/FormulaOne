import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DriversTable } from './drivers-table';

describe('DriversTable', () => {
  let component: DriversTable;
  let fixture: ComponentFixture<DriversTable>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DriversTable]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DriversTable);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
