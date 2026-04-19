import { ChangeDetectorRef, Component } from '@angular/core';
import { RentalResponseForRenterDTO } from '../../../../types/rentalResponseForRenterTypes';
import { RentalService } from '../../../services/rental-service';
import { NgClass, NgFor, NgIf } from '@angular/common';
import { environment } from '../../../../environments/environment';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-renter-view-rentals',
  imports: [NgClass, NgFor,RouterLink],
  templateUrl: './renter-view-rentals.html',
  styleUrl: './renter-view-rentals.css',
})
export class RenterViewRentals {
    rentals:RentalResponseForRenterDTO[]=[];
    url=environment.apiUrl;
    showRentalForm:boolean=false;
    selectedPropertyId:number=0;

    constructor(private rentalService:RentalService,private cdr:ChangeDetectorRef){
        
    }

    ngOnInit(){
        this.rentalService.getRentalsOfRenter().subscribe({
            next:(res)=>{
                this.rentals=res as RentalResponseForRenterDTO[];
                this.cdr.detectChanges();
                console.log(this.rentals);
            },
            error:(err)=>{
                console.log(err);
            }
        })
    }

    deleteRental(propertyId:number){
      this.rentalService.deleteRental(propertyId).subscribe({
        next:(res)=>{
          console.log(res);
          alert('Rental deleted');
          this.ngOnInit();
        },
        error:(err)=>{
          console.log(err);
        }
      })
    }
    
}
