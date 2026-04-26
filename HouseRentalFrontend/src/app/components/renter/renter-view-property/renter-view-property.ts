import { ChangeDetectorRef, Component } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { PropertyResponseDTO } from '../../../../types/propertyTypes';
import { PropertyService } from '../../../services/property-service';
import { NgFor } from '@angular/common';
import { RouterLink } from '@angular/router';
import { FilterComponent } from '../filter-component/filter-component';

@Component({
  selector: 'app-renter-view-property',
  imports: [NgFor,RouterLink,FilterComponent],
  templateUrl: './renter-view-property.html',
  styleUrl: './renter-view-property.css',
})
export class RenterViewProperty {
   photoURL=environment.apiUrl;

  properties: PropertyResponseDTO[] = [];

  constructor(
    private propertyService: PropertyService,
    private cdr: ChangeDetectorRef
  ) { }

  ngOnInit(): void {
    this.loadProperties();
  }

  loadProperties() {
    this.propertyService.getAllProperties().subscribe({
      next: (res) => {
        this.properties = [...res];
        this.cdr.detectChanges();
      },
      error: (err) => {
        console.log(err);
      }
    })
  }

  loadFilteredProperties(filteredProperties:PropertyResponseDTO[]){
    this.properties=[...filteredProperties];
    this.cdr.detectChanges();
  }

}
