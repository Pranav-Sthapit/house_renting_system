import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OwnerPropertyDetail } from './owner-property-detail';

describe('OwnerPropertyDetail', () => {
  let component: OwnerPropertyDetail;
  let fixture: ComponentFixture<OwnerPropertyDetail>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [OwnerPropertyDetail]
    })
    .compileComponents();

    fixture = TestBed.createComponent(OwnerPropertyDetail);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
