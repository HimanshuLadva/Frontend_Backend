import { TestBed } from '@angular/core/testing';

import { TeamdataService } from './teamdata.service';

describe('TeamdataService', () => {
  let service: TeamdataService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(TeamdataService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
