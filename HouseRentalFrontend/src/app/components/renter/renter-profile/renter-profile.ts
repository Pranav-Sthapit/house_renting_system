import { ChangeDetectorRef, Component } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { RenterResponseDTO } from '../../../../types/personTypes';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ProfileService } from '../../../services/profile-service';
import { NgIf } from '@angular/common';

@Component({
  selector: 'app-renter-profile',
  imports: [ReactiveFormsModule, NgIf],
  templateUrl: './renter-profile.html',
  styleUrl: './renter-profile.css',
})
export class RenterProfile {
  url = environment.apiUrl;

  renterProfile: RenterResponseDTO | null = null;

  profileUpdateForm: FormGroup = new FormGroup({
    firstName: new FormControl('', [Validators.required, Validators.pattern(/^[a-zA-Z]+$/), Validators.minLength(3)]),
    lastName: new FormControl('', [Validators.required, Validators.pattern(/^[a-zA-Z]+$/), Validators.minLength(3)]),
    email: new FormControl('', [Validators.required, Validators.email]),
    address: new FormControl('', [Validators.required]),
    contact: new FormControl('', [Validators.required, Validators.pattern(/^[9][0-9]{9}$/)]),
    profilePhoto: new FormControl<File | null>(null),
    passport: new FormControl<File | null>(null),
    citizenship: new FormControl<File | null>(null),
    username: new FormControl('', [Validators.required, Validators.pattern(/^[a-zA-Z0-9]+$/), Validators.minLength(3), Validators.maxLength(20)]),
    password: new FormControl('', [Validators.required, Validators.pattern(/^[a-zA-Z0-9]+$/), Validators.minLength(6), Validators.maxLength(20)])
  })

  constructor(private profileService: ProfileService, private cdr: ChangeDetectorRef) {

  }

  ngOnInit() {
    this.profileService.getRenterProfile().subscribe({
      next: (res) => {

        this.renterProfile = res as RenterResponseDTO;
        this.profileUpdateForm.patchValue({
          firstName: this.renterProfile.firstName,
          lastName: this.renterProfile.lastName,
          email: this.renterProfile.email,
          address: this.renterProfile.address,
          contact: this.renterProfile.contact,
          username: this.renterProfile.username,
          password: this.renterProfile.password,
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

  onPassportChange(event: any) {
    const file = event.target.files[0];
    if (!file.type.startsWith('image/')) {
      alert("Only image files are allowed!");
      event.target.value = '';
      return;
    }
    this.profileUpdateForm.get('passport')?.setValue(file);
  }

  onCitizenshipChange(event: any) {
    const file = event.target.files[0];
    if (!file.type.startsWith('image/')) {
      alert("Only image files are allowed!");
      event.target.value = '';
      return;
    }
    this.profileUpdateForm.get('citizenship')?.setValue(file);
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

    const profilePhoto = this.profileUpdateForm.value.profilePhoto;
    const citizenship = this.profileUpdateForm.value.citizenship;
    const passport = this.profileUpdateForm.value.passport;

    if (profilePhoto instanceof File) {
      formData.append('profilePhoto', profilePhoto);
    }
    if (citizenship instanceof File) {
      formData.append('citizenship', citizenship);
    }
    if (passport instanceof File) {
      formData.append('passport', passport);
    }

    this.profileService.updateRenterInfo(formData).subscribe({
      next: (res) => {
        alert("Profile updated successfully");
        this.renterProfile = res as RenterResponseDTO;
        this.cdr.detectChanges();
      },
      error: (err) => {
        console.log(err);
        alert("Profile update failed");
      }
    })
  }



}
