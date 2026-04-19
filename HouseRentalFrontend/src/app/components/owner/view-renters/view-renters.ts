import { ChangeDetectorRef, Component } from '@angular/core';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { RentalService } from '../../../services/rental-service';
import { RentalResponseForOwnerDTO } from '../../../../types/rentalResponseForOwnerTypes';
import { NgClass, NgFor, NgIf } from '@angular/common';

@Component({
  selector: 'app-view-renters',
  imports: [NgIf, NgFor, RouterLink, NgClass],
  templateUrl: './view-renters.html',
  styleUrl: './view-renters.css',
})
export class ViewRenters {

  propertyId: number | null = null;
  renterList: RentalResponseForOwnerDTO[] | null = null;

  constructor(private route: ActivatedRoute, private rentalService: RentalService, private cdr: ChangeDetectorRef) {

  }

  ngOnInit() {
    this.route.params.subscribe(params => {
      const propertyId = params['id'];
      this.propertyId = propertyId;
      this.rentalService.getRenters(propertyId).subscribe(res => {
        console.log(res);
        this.renterList = res as RentalResponseForOwnerDTO[];
        this.cdr.detectChanges();
      });
    });
  }


  acceptRenter(renterId: number, renter: RentalResponseForOwnerDTO) {
    console.log("Accepted renter:", renterId);

    if (this.propertyId) {
      this.rentalService.acceptRenter(this.propertyId, renterId).subscribe(res => {
        alert("renter accepted");
        renter.status = "approved";
        this.cdr.detectChanges();
      });
    }
  }


  rejectRenter(renterId: number, renter: RentalResponseForOwnerDTO) {
    console.log("Rejected renter:", renterId);

    if (this.propertyId) {
      this.rentalService.rejectRenter(this.propertyId, renterId).subscribe(res => {
        alert("renter rejected");
        renter.status = "rejected";
        this.cdr.detectChanges();
      });
    }


  }

}
