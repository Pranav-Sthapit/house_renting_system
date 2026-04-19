import { ChangeDetectorRef, Component } from '@angular/core';
import { FormControl, FormGroup, FormsModule, Validators, ReactiveFormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { PropertyResponseDTO } from '../../../../types/propertyTypes';
import { PropertyService } from '../../../services/property-service';
import { NgIf } from '@angular/common';
import { environment } from '../../../../environments/environment';

@Component({
  selector: 'app-owner-property-detail',
  imports: [FormsModule, ReactiveFormsModule, NgIf],
  templateUrl: './owner-property-detail.html',
  styleUrl: './owner-property-detail.css',
})
export class OwnerPropertyDetail {
  propertyId!: number;

  property: PropertyResponseDTO | null = null;

  updatePropertyForm: FormGroup = new FormGroup({
    bhk: new FormControl(0, [Validators.required, Validators.pattern(/^[1-9]/)]),
    rent: new FormControl(0, [Validators.required, Validators.pattern(/^[1-9][0-9]*$/)]),
    size: new FormControl(0, [Validators.required, Validators.pattern(/^[1-9][0-9]*$/)]),
    floor: new FormControl("", [Validators.required, Validators.pattern(/^[a-zA-Z0-9 ]+$/)]),
    areaType: new FormControl("", Validators.required),
    locality: new FormControl("", [Validators.required, Validators.pattern(/^[a-zA-Z ]+$/)]),
    city: new FormControl("", [Validators.required, Validators.pattern(/^[a-zA-Z ]+$/)]),
    furnishingStatus: new FormControl("", Validators.required),
    tenant: new FormControl("", [Validators.required, Validators.pattern(/^[a-zA-Z ]+$/)]),
    bathroom: new FormControl(0, [Validators.required, Validators.pattern(/^[1-9][0-9]*$/)]),
    thumbnail: new FormControl<File | null>(null),
    aggrement: new FormControl<File | null>(null)
  });

  areaTypes: string[] = [
    'Carpet Area',
    'Built-up Area',
    'Super Built-up Area'
  ];

  constructor(private route: ActivatedRoute,
    private router: Router,
    private propertyService: PropertyService,
    private cdr: ChangeDetectorRef) { }

  ngOnInit(): void {
    this.propertyId = Number(this.route.snapshot.paramMap.get('id'));

    if (this.propertyId == null || !this.propertyId) {
      alert('error occured while viewing property detail');
      this.router.navigate(['/owner-home']);
    }

    this.propertyService.getPropertyDetails(this.propertyId).subscribe({
      next: (data) => {

        data.thumbnail = environment.apiUrl + data.thumbnail;
        data.aggrementOfTerms = environment.apiUrl + data.aggrementOfTerms;

        this.property = data;

        this.updatePropertyForm.patchValue({
          bhk: data.bhk,
          rent: data.rent,
          size: data.size,
          floor: data.floor,
          areaType: data.areaType,
          locality: data.locality,
          city: data.city,
          furnishingStatus: data.furnishingStatus,
          tenant: data.tenant,
          bathroom: data.bathroom
        });

        this.cdr.detectChanges();

      },
      error: (err) => {
        console.log(err);
      }
    });
  }

  onThumbnailChange(event: any) {
    const file = event.target.files[0];

    if (!file || !file.type.startsWith('image/')) {
      event.target.value = '';
      this.updatePropertyForm.get('thumbnail')?.markAsTouched();
      this.updatePropertyForm.get('thumbnail')?.setErrors({ invalidFile: true });
      return;
    }

    this.updatePropertyForm.get('thumbnail')?.setValue(file);
  }

  onAggrementChange(event: any) {
    const file = event.target.files[0];

    if (!file || !file.type.startsWith('application/pdf')) {
      event.target.value = '';
      this.updatePropertyForm.get('aggrement')?.markAsTouched();
      this.updatePropertyForm.get('aggrement')?.setErrors({ invalidFile: true });
      return;
    }
    this.updatePropertyForm.get('aggrement')?.setValue(file);
  }


  saveChanges() {

    if (!this.updatePropertyForm.valid) {
      this.updatePropertyForm.markAllAsTouched();
      return;
    }

    const formData = new FormData();
    formData.append('bhk', this.updatePropertyForm.get('bhk')?.value);
    formData.append('rent', this.updatePropertyForm.get('rent')?.value);
    formData.append('size', this.updatePropertyForm.get('size')?.value);
    formData.append('floor', this.updatePropertyForm.get('floor')?.value);
    formData.append('areaType', this.updatePropertyForm.get('areaType')?.value);
    formData.append('locality', this.updatePropertyForm.get('locality')?.value);
    formData.append('city', this.updatePropertyForm.get('city')?.value);
    formData.append('furnishingStatus', this.updatePropertyForm.get('furnishingStatus')?.value);
    formData.append('tenant', this.updatePropertyForm.get('tenant')?.value);
    formData.append('bathroom', this.updatePropertyForm.get('bathroom')?.value);
    if (this.updatePropertyForm.get('thumbnail')?.value) {
      formData.append('thumbnail', this.updatePropertyForm.get('thumbnail')?.value);
    }
    if (this.updatePropertyForm.get('agreement')?.value) {
      formData.append('aggrementOfTerms', this.updatePropertyForm.get('agreement')?.value);
    }

    this.propertyService.updatePropertyDetails(this.propertyId, formData).subscribe({
      next: (res) => {
        alert("Property updated successfully");

        res.thumbnail = environment.apiUrl + res.thumbnail;
        res.aggrementOfTerms = environment.apiUrl + res.aggrementOfTerms;
        this.property = res;
        this.cdr.detectChanges();

      },
      error: (err) => {
        console.error(err);
        alert("Failed to update property");
      }
    })

  }

  deleteProperty() {
    const confirmDelete = confirm("Are you sure you want to delete this property?");

    if (!confirmDelete) return;

    this.propertyService.deleteProperty(this.propertyId).subscribe({
      next: (res) => {
        alert("Property deleted successfully");
        this.router.navigate(['/owner-home/view-property']);
      },
      error: (err) => {
        console.error(err);
        alert("Failed to delete property");
      }
    })
  }
}
