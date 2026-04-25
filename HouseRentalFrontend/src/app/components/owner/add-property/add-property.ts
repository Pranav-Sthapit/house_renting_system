import { Component } from '@angular/core';
import { PropertyRequestDTO } from '../../../../types/propertyTypes';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { NgFor, NgIf } from '@angular/common';
import { PropertyService } from '../../../services/property-service';
import { Map } from '../../map/map';

@Component({
  selector: 'app-add-property',
  imports: [ReactiveFormsModule, NgIf, NgFor, Map],
  templateUrl: './add-property.html',
  styleUrl: './add-property.css',
})
export class AddProperty {

  addPropertyForm: FormGroup = new FormGroup({
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
    thumbnail: new FormControl<File | null>(null, Validators.required),
    agreement: new FormControl<File | null>(null, Validators.required),
    latitude: new FormControl<number | null>(null, Validators.required),
    longitude: new FormControl<number | null>(null, Validators.required)
  });

  fileList: { file: File, preview: string }[] = [];


  constructor(private propertyService: PropertyService) { }


  thumbnailPreview: string | null = null;

  onThumbnailChange(event: any) {
    const file = event.target.files[0];

    if (!file || !file.type.startsWith('image/')) {
      event.target.value = '';
      this.addPropertyForm.get('agreement')?.setErrors({ invalidFile: true });
      return;
    }

    this.addPropertyForm.get('thumbnail')?.setValue(file);
    this.thumbnailPreview = URL.createObjectURL(file);
  }

  onAggrementChange(event: any) {
    const file = event.target.files[0];

    if (!file || !file.type.startsWith('application/pdf')) {
      event.target.value = '';
      this.addPropertyForm.get('agreement')?.setErrors({ invalidFile: true });
      return;
    }

    this.addPropertyForm.get('agreement')?.setValue(file);
  }

  onPictureAdded(event: any) {
    const fileList = event.target.files;
    let isValid = true;
    for (const file of fileList) {
      if (!file.type.startsWith('image/')) {
        isValid = false;
        continue;
      }
      this.fileList.push({ file, preview: URL.createObjectURL(file) });
    }
    if (!isValid) {
      alert("Non image files are removed");
      event.target.value = '';
    }
  }

  removePic(index: number) {
    this.fileList.splice(index, 1);
  }

  setLatLang(latlng:{lat:number,lng:number}|null){
    if(latlng){
      this.addPropertyForm.get('latitude')?.setValue(latlng.lat);
      this.addPropertyForm.get('longitude')?.setValue(latlng.lng);
      this.addPropertyForm.get('latitude')?.setErrors(null);
      this.addPropertyForm.get('longitude')?.setErrors(null);
    }else{
      this.addPropertyForm.get('latitude')?.setErrors({required:true});
      this.addPropertyForm.get('longitude')?.setErrors({required:true});
    }
  }


  addProperty() {
    if (!this.addPropertyForm.valid) {
      this.addPropertyForm.markAllAsTouched();
      return;
    }

    const formData = new FormData();
    formData.append('bhk', this.addPropertyForm.get('bhk')?.value);
    formData.append('rent', this.addPropertyForm.get('rent')?.value);
    formData.append('size', this.addPropertyForm.get('size')?.value);
    formData.append('floor', this.addPropertyForm.get('floor')?.value);
    formData.append('areaType', this.addPropertyForm.get('areaType')?.value);
    formData.append('locality', this.addPropertyForm.get('locality')?.value);
    formData.append('city', this.addPropertyForm.get('city')?.value);
    formData.append('furnishingStatus', this.addPropertyForm.get('furnishingStatus')?.value);
    formData.append('tenant', this.addPropertyForm.get('tenant')?.value);
    formData.append('bathroom', this.addPropertyForm.get('bathroom')?.value);
    formData.append('thumbnail', this.addPropertyForm.get('thumbnail')?.value);
    formData.append('aggrementOfTerms', this.addPropertyForm.get('agreement')?.value);

    this.fileList.forEach((item) => {
      formData.append('pictures', item.file);
    });

    formData.append('latitude', this.addPropertyForm.get('latitude')?.value.toString());
    formData.append('longitude', this.addPropertyForm.get('longitude')?.value.toString());

    formData.forEach((value,key)=>{
      console.log(key,value);
    })

    this.propertyService.addProperty(formData).subscribe({
      next: (res) => {
        alert("Property added successfully");
        console.log(res);
      },
      error: (err) => {
        console.error(err);
        alert("Failed to add property");
      }
    })
  }




}
