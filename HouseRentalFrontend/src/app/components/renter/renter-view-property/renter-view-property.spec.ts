import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RenterViewProperty } from './renter-view-property';

describe('RenterViewProperty', () => {
  let component: RenterViewProperty;
  let fixture: ComponentFixture<RenterViewProperty>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RenterViewProperty]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RenterViewProperty);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
