import { TestBed } from '@angular/core/testing';

import { ConstrCshService } from './constr-csh-service';

describe('ConstrCshService', () => {
  let service: ConstrCshService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ConstrCshService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
