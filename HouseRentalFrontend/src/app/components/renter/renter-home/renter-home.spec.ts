import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RenterHome } from './renter-home';

describe('RenterHome', () => {
  let component: RenterHome;
  let fixture: ComponentFixture<RenterHome>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RenterHome]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RenterHome);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
