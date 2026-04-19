import { ChangeDetectorRef, Component } from '@angular/core';
import { RentalService } from '../../../services/rental-service';
import { ActivatedRoute } from '@angular/router';
import { RentalResponseForOwnerWithDetailsDTO } from '../../../../types/rentalResponseForOwnerTypes';
import { NgClass, NgIf } from '@angular/common';

@Component({
  selector: 'app-renter-details-by-owner',
  imports: [NgIf,NgClass],
  templateUrl: './renter-details-by-owner.html',
  styleUrl: './renter-details-by-owner.css',
})
export class RenterDetailsByOwner {

  propertyId:number|null=null;
  renterId:number|null=null;

  renterDetails:RentalResponseForOwnerWithDetailsDTO|null=null;

  constructor(private rentalService:RentalService,private route:ActivatedRoute,private cdr:ChangeDetectorRef){ }

  ngOnInit(){
    this.route.params.subscribe(params => {
                const propertyId = params['propertyId'];
                const renterId=params['renterId'];
                this.propertyId=propertyId;
                this.renterId=renterId;
                this.rentalService.getRenterDetails(propertyId,renterId).subscribe(res => {
                    console.log(res); 
                    this.renterDetails=res as RentalResponseForOwnerWithDetailsDTO;
                    this.cdr.detectChanges();
                });
            });
  }



  acceptRenter(){
    console.log("Accepted renter:", this.renterId);
  
    if(this.renterId && this.propertyId){
      this.rentalService.acceptRenter(this.propertyId,this.renterId).subscribe(res => {
        alert("renter accepted");
        this.renterDetails=res as RentalResponseForOwnerWithDetailsDTO;
        this.cdr.detectChanges();
      });
    }
  }  
  
  
  rejectRenter(){
    console.log("Rejected renter:", this.renterId);
  
    if(this.propertyId && this.renterId){
      this.rentalService.rejectRenter(this.propertyId,this.renterId).subscribe(res => {
        alert("renter rejected");
        this.renterDetails=res as RentalResponseForOwnerWithDetailsDTO;
        this.cdr.detectChanges();
      });
    }
  }
}
