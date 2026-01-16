import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ConstructorsTable } from './constructors-table';

describe('ConstructorsTable', () => {
  let component: ConstructorsTable;
  let fixture: ComponentFixture<ConstructorsTable>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ConstructorsTable]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ConstructorsTable);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
