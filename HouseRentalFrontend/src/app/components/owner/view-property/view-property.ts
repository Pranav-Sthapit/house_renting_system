import { Component, ChangeDetectorRef } from '@angular/core';
import { PropertyResponseDTO } from '../../../../types/propertyTypes';
import { NgFor } from '@angular/common';
import { RouterLink } from '@angular/router';
import { PropertyService } from '../../../services/property-service';
import { environment } from '../../../../environments/environment';
@Component({
  selector: 'app-view-property',
  imports: [NgFor, RouterLink],
  templateUrl: './view-property.html',
  styleUrl: './view-property.css',
})
export class ViewProperty {

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
    this.propertyService.getOwnerProperty().subscribe({
      next: (res) => {
        this.properties = [...res];
        this.cdr.detectChanges();
      },
      error: (err) => {
        console.log(err);
      }
    })
  }

}

