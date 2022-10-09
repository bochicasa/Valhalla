import { TestBed } from '@angular/core/testing';

import { Basket.WrapperService } from './basket.wrapper.service';

describe('Basket.WrapperService', () => {
  let service: Basket.WrapperService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(Basket.WrapperService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
