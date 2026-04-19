import { ChangeDetectorRef, Component } from '@angular/core';
import { RentalResponseForRenterWithDetailsDTO } from '../../../../types/rentalResponseForRenterTypes';
import { ActivatedRoute, Router } from '@angular/router';
import { RentalService } from '../../../services/rental-service';
import { environment } from '../../../../environments/environment';
import { NgIf, NgClass } from '@angular/common';
import { RentalForm } from "../rental-form/rental-form";

@Component({
  selector: 'app-rental-and-property-details',
  imports: [NgIf, RentalForm,NgClass],
  templateUrl: './rental-and-property-details.html',
  styleUrl: './rental-and-property-details.css',
})
export class RentalAndPropertyDetails {

    url=environment.apiUrl;

    showRentalForm:boolean=false;

    rentalWithPropertyDetails:RentalResponseForRenterWithDetailsDTO|null=null;

    propertyId:number=0;
    
    constructor(private rentalService:RentalService,private route:ActivatedRoute,private cdr:ChangeDetectorRef,private router:Router){
        
    }

    ngOnInit(){
        this.route.paramMap.subscribe(params=>{
            const propertyId=Number(params.get('propertyId'));
            this.propertyId=propertyId
            this.rentalService.getRentalAndPropertyDetails(propertyId).subscribe({
                next:(res)=>{
                  console.log(res);
                    this.rentalWithPropertyDetails=res as RentalResponseForRenterWithDetailsDTO;
                    this.cdr.detectChanges();
                },
                error:(err)=>{
                    console.log(err);
                }
            })
        })
    }

    updateRental(){
      this.showRentalForm=true;
    }

    deleteRental(){
      this.rentalService.deleteRental(this.propertyId).subscribe({
        next:(res)=>{
          console.log(res);
          alert('Rental deleted');
          this.router.navigate(['/renter-home/renter-view-rentals']);
        },
        error:(err)=>{
          console.log(err);
        }
      })
    }

}
