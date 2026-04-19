import { ChangeDetectorRef, Component } from '@angular/core';
import { OwnerResponseDTO } from '../../../../types/personTypes';
import { HttpClient } from '@angular/common/http';
import { ProfileService } from '../../../services/profile-service';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { NgIf } from '@angular/common';
import { environment } from '../../../../environments/environment';

@Component({
  selector: 'app-owner-profile',
  imports: [NgIf, ReactiveFormsModule],
  templateUrl: './owner-profile.html',
  styleUrl: './owner-profile.css',
})
export class OwnerProfile {

  url = environment.apiUrl;

  ownerProfile: OwnerResponseDTO | null = null;

  profileUpdateForm: FormGroup = new FormGroup({
    firstName: new FormControl('', [Validators.required, Validators.pattern(/^[a-zA-Z]+$/), Validators.minLength(3)]),
    lastName: new FormControl('', [Validators.required, Validators.pattern(/^[a-zA-Z]+$/), Validators.minLength(3)]),
    email: new FormControl('', [Validators.required, Validators.email]),
    address: new FormControl('', [Validators.required]),
    contact: new FormControl('', [Validators.required, Validators.pattern(/^[9][0-9]{9}$/)]),
    profilePhoto: new FormControl<File | null>(null),
    username: new FormControl('', [Validators.required, Validators.pattern(/^[a-zA-Z0-9]+$/), Validators.minLength(3), Validators.maxLength(20)]),
    password: new FormControl('', [Validators.required, Validators.pattern(/^[a-zA-Z0-9]+$/), Validators.minLength(6), Validators.maxLength(20)])
  })

  constructor(private profileService: ProfileService, private cdr: ChangeDetectorRef) {

  }

  ngOnInit() {
    this.profileService.getOwnerProfile().subscribe({
      next: (res) => {
        this.ownerProfile = res as OwnerResponseDTO;
        this.profileUpdateForm.patchValue({
          firstName: this.ownerProfile.firstName,
          lastName: this.ownerProfile.lastName,
          email: this.ownerProfile.email,
          address: this.ownerProfile.address,
          contact: this.ownerProfile.contact,
          username: this.ownerProfile.username,
          password: this.ownerProfile.password,
        });
        this.cdr.detectChanges();

      },
      error: (err) => {
        console.log(err);
      }
    })
  }

  onProfilePhotoChange(event: any) {
    const file = event.target.files[0];
    if (!file.type.startsWith('image/')) {
      alert("Only image files are allowed!");
      event.target.value = '';
      return;
    }
    this.profileUpdateForm.get('profilePhoto')?.setValue(file);
  }

  updateProfile() {
    if (!this.profileUpdateForm.valid) {
      this.profileUpdateForm.markAllAsTouched();
      return;
    }

    const formData = new FormData();
    formData.append('firstName', this.profileUpdateForm.value.firstName as string);
    formData.append('lastName', this.profileUpdateForm.value.lastName as string);
    formData.append('email', this.profileUpdateForm.value.email as string);
    formData.append('address', this.profileUpdateForm.value.address as string);
    formData.append('contact', this.profileUpdateForm.value.contact as string);
    formData.append('username', this.profileUpdateForm.value.username as string);
    formData.append('password', this.profileUpdateForm.value.password as string);

    const file = this.profileUpdateForm.value.profilePhoto;

    if (file instanceof File) {
      formData.append('profilePhoto', file);
    }

    this.profileService.updateOwnerInfo(formData).subscribe({
      next: (res) => {
        alert("Profile updated successfully");
        this.ownerProfile = res as OwnerResponseDTO;
        this.cdr.detectChanges();
      },
      error: (err) => {
        console.log(err);
        alert("Profile update failed");
      }
    })
  }



}
