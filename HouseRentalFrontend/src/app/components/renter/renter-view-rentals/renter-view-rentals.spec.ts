import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RenterViewRentals } from './renter-view-rentals';

describe('RenterViewRentals', () => {
  let component: RenterViewRentals;
  let fixture: ComponentFixture<RenterViewRentals>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RenterViewRentals]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RenterViewRentals);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
