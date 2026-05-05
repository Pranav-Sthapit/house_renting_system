import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RentalService } from '../../../services/rental-service';
import { Router } from '@angular/router';
import { BehaviourService } from '../../../services/behaviour-service';

@Component({
  selector: 'app-rental-form',
  imports: [FormsModule],
  templateUrl: './rental-form.html',
  styleUrl: './rental-form.css',
})
export class RentalForm {
  @Output() closeForm = new EventEmitter<void>();
  @Input() type = '';
  @Input() propertyId = 0;
  tenant = ''
  rent = 0

  constructor(private rentalService: RentalService,
    private behaviourService: BehaviourService,
    private router:Router) { }

  apply() {
    console.log(this.tenant);
    console.log(this.rent);
    console.log(this.type);
    console.log(this.propertyId);

    if(this.tenant.trim()=='' || this.rent==0){
      alert('please fill all the fields');
      return;
    }else if(this.rent<0){
      alert('rent cannot be negative');
      return;
    }


    if (this.type == "apply") {

      this.rentalService.addRental(this.propertyId, this.tenant, this.rent).subscribe({
        next: (res) => {
          console.log(res);
          alert('rental applied');
          this.closeForm.emit();
          this.router.navigate(['/renter-home/view-rentals']);
        },
        error: (err) => {
          console.log(err);
          alert('failed to apply for rental');
        }
      })

      this.behaviourService.applyForProperty(this.propertyId).subscribe({
        next: (res) => {
          console.log("applied");
        },
        error: (err) => {
          console.log(err);
        }
      });


    }

    if (this.type == "update") {
      this.rentalService.updateRental(this.propertyId, this.tenant, this.rent).subscribe({
        next: (res) => {
          console.log(res);
          alert('rental updated');
          this.closeForm.emit();
          this.router.navigate(['/renter-home/view-rentals']);
        },
        error: (err) => {
          console.log(err);
          alert('failed to update rental');
        }
      })
    }


  }


} 
