import { TestBed } from '@angular/core/testing';

import { AddressdataService } from './addressdata.service';

describe('AddressdataService', () => {
  let service: AddressdataService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AddressdataService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
