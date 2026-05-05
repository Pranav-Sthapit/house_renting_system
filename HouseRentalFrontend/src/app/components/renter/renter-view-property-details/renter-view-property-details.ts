import { ChangeDetectorRef, Component } from '@angular/core';
import { PropertyResponseDTO } from '../../../../types/propertyTypes';
import { ActivatedRoute, Router } from '@angular/router';
import { PropertyService } from '../../../services/property-service';
import { environment } from '../../../../environments/environment';
import { NgFor, NgIf } from '@angular/common';
import { RentalForm } from '../rental-form/rental-form';
import { Map } from '../../map/map';
import { BehaviourService } from '../../../services/behaviour-service';

@Component({
  selector: 'app-renter-view-property-details',
  imports: [NgIf, RentalForm, NgFor, Map],
  templateUrl: './renter-view-property-details.html',
  styleUrl: './renter-view-property-details.css',
})
export class RenterViewPropertyDetails {
  propertyId!: number;

  showRentalForm: boolean = false;
  type: string = "apply";

  property: PropertyResponseDTO | null = null;

  constructor(private route: ActivatedRoute,
    private router: Router,
    private propertyService: PropertyService,
    private cdr: ChangeDetectorRef,
    private behaviourService: BehaviourService) { }


  ngOnInit(): void {
    this.propertyId = Number(this.route.snapshot.paramMap.get('id'));

    if (this.propertyId == null || !this.propertyId) {
      alert('error occured while viewing property detail');
      this.router.navigate(['/owner-home']);
    }

    this.behaviourService.incrementViewCount(this.propertyId).subscribe({
      next: (data) => {
        console.log("view count incremented");
      },
      error: (err) => {
        console.log(err);
      }
    });

    this.propertyService.getPropertyDetails(this.propertyId).subscribe({
      next: (data) => {
        data.thumbnail = environment.apiUrl + data.thumbnail;
        data.aggrementOfTerms = environment.apiUrl + data.aggrementOfTerms;
        data.pictures = data.pictures.map((picture: string) => environment.apiUrl + picture);

        this.property = data;
        this.cdr.detectChanges();

      },
      error: (err) => {
        console.log(err);
      }
    });
  }

  applyForProperty() {
    this.showRentalForm = true;
    this.type = "apply";
  }

  closeRentalForm() {
    this.showRentalForm = false;
  }


}
