import { TestBed } from '@angular/core/testing';

import { DriversCshService } from './drivers-csh-service';

describe('DriversCshService', () => {
  let service: DriversCshService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(DriversCshService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
