import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RenterProfile } from './renter-profile';

describe('RenterProfile', () => {
  let component: RenterProfile;
  let fixture: ComponentFixture<RenterProfile>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RenterProfile]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RenterProfile);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
