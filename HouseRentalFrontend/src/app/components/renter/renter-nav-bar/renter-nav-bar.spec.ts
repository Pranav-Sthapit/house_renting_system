import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RenterNavBar } from './renter-nav-bar';

describe('RenterNavBar', () => {
  let component: RenterNavBar;
  let fixture: ComponentFixture<RenterNavBar>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RenterNavBar]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RenterNavBar);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
