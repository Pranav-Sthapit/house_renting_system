import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RentalAndPropertyDetails } from './rental-and-property-details';

describe('RentalAndPropertyDetails', () => {
  let component: RentalAndPropertyDetails;
  let fixture: ComponentFixture<RentalAndPropertyDetails>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RentalAndPropertyDetails]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RentalAndPropertyDetails);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
