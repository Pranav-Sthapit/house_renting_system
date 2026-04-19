import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewRenters } from './view-renters';

describe('ViewRenters', () => {
  let component: ViewRenters;
  let fixture: ComponentFixture<ViewRenters>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ViewRenters]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ViewRenters);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
