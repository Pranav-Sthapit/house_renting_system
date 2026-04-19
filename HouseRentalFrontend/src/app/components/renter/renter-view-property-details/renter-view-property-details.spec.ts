import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RenterViewPropertyDetails } from './renter-view-property-details';

describe('RenterViewPropertyDetails', () => {
  let component: RenterViewPropertyDetails;
  let fixture: ComponentFixture<RenterViewPropertyDetails>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RenterViewPropertyDetails]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RenterViewPropertyDetails);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
