import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RenterDetailsByOwner } from './renter-details-by-owner';

describe('RenterDetailsByOwner', () => {
  let component: RenterDetailsByOwner;
  let fixture: ComponentFixture<RenterDetailsByOwner>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RenterDetailsByOwner]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RenterDetailsByOwner);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
