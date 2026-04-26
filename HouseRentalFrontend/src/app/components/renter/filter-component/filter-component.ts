import { Component, EventEmitter, Output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { NgFor } from '@angular/common';
import { PropertyService } from '../../../services/property-service';
import { PropertyResponseDTO } from '../../../../types/propertyTypes';

@Component({
  selector: 'app-filter-component',
  imports: [FormsModule, NgFor],
  templateUrl: './filter-component.html',
  styleUrl: './filter-component.css',
})
export class FilterComponent {

  @Output() filteredProperties = new EventEmitter<PropertyResponseDTO[]>();

  constructor(private propertyService: PropertyService) { }

  filters = [
    {
      name: 'bhk',
      keyName: 'bhk',
      options: [
        { label: "1 BHK", value: "1" },
        { label: "2 BHK", value: "2" },
        { label: "3 BHK", value: "3" },
        { label: "4 BHK +", value: "4" },
      ],
      value: ''
    },
    {
      name: 'rent',
      keyName: 'rent',
      options: [
        { label: "Low Range", value: "10000" },
        { label: "Mid Range", value: "16000" },
        { label: "High Range", value: "34000" },
        { label: "Luxury", value: "50000" },
      ],
      value: ''
    }, {
      name: 'size',
      keyName: 'size',
      options: [
        { label: "Small", value: "550" },
        { label: "Medium", value: "850" },
        { label: "Large", value: "1200" },
        { label: "Extra Large", value: "1600" },
      ],
      value: ''
    }, {
      name: 'tenant',
      keyName: 'tenant_preferred',
      options: [
        { label: "Bachelors", value: "Bachelors" },
        { label: "Bachelors/Family", value: "Bachelors/Family" },
        { label: "Family", value: "Family" },
      ],
      value: ''
    }, {
      name: 'area type',
      keyName: 'area_Type',
      options: [
        { label: "Carpet Area", value: "Carpet Area" },
        { label: "Built Area", value: "Built Area" },
        { label: "Super Area", value: "Super Area" },
      ],
      value: ''
    }, {
      name: 'furnishing status',
      keyName: 'furnishing_Status',
      options: [
        { label: "Unfurnished", value: "Unfurnished" },
        { label: "Semi-Furnished", value: "Semi-Furnished" },
        { label: "Furnished", value: "Furnished" },
      ],
      value: ''
    }, {
      name: 'floor',
      keyName: 'floor',
      options: [
        { label: "ground floor", value: "Ground out of 4" },
        { label: "low floor", value: "1 out of 4" },
        { label: "mid floor", value: "2 out of 4" },
        { label: "high floor", value: "3 out of 4" },
        { label: "top floor", value: "4 out of 4" },
      ],
      value: ''
    }
  ];


  filterData() {
    let doFilter=false;
      this.filters.forEach(f => {
        if(f.value!=''){
          doFilter=true;
        }
      });
    if(!doFilter){
      alert("Please select at least one filter");
      return;
    }

    const body = this.filters.reduce((obj: any, item: any) => {
      if (item.value !== "") {
        if (!isNaN(Number(item.value))) {
          obj[item.keyName] = Number(item.value);
        } else {
          obj[item.keyName] = item.value;
        }
      } else {
        obj[item.keyName] = null;
      }
      return obj;
    }, {});

    this.propertyService.getFilteredProperties(body).subscribe({
      next: (res: PropertyResponseDTO[]) => {
        this.filteredProperties.emit(res);
      },
      error: (err: any) => {
        console.log(err);
        alert(err.error.message);
      }
    })

  }
}
