import { TestBed } from '@angular/core/testing';

import { LoginAndRegister } from './login-and-register';

describe('LoginAndRegister', () => {
  let service: LoginAndRegister;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(LoginAndRegister);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
