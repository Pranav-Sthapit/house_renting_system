import { Component } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { NgIf } from '@angular/common';
import { Router, RouterLink } from '@angular/router';
import { LoginAndRegister } from '../../services/login-and-register';

@Component({
  selector: 'app-register',
  imports: [ReactiveFormsModule, NgIf, RouterLink],
  templateUrl: './register.html',
  styleUrl: './register.css',
})
export class Register {

  registerForm = new FormGroup({
    firstName: new FormControl('', [Validators.required, Validators.pattern(/^[a-zA-Z]+$/), Validators.minLength(3)]),
    lastName: new FormControl('', [Validators.required, Validators.pattern(/^[a-zA-Z]+$/), Validators.minLength(3)]),
    email: new FormControl('', [Validators.required, Validators.email]),
    address: new FormControl('', [Validators.required]),
    contact: new FormControl('', [Validators.required, Validators.pattern(/^[9][0-9]{9}$/)]),
    profilePhoto: new FormControl<File | null>(null),
    username: new FormControl('', [Validators.required, Validators.pattern(/^[a-zA-Z0-9]+$/), Validators.minLength(3), Validators.maxLength(20)]),
    password: new FormControl('', [Validators.required, Validators.pattern(/^\S+$/), Validators.minLength(6), Validators.maxLength(20)]),
    category: new FormControl('', [Validators.required])
  })

  get form() {
    return this.registerForm.controls;
  }

  constructor(private loginAndRegister: LoginAndRegister, private router: Router) { }

  onProfilePhotoChange(event: any) {
    const file = event.target.files[0];
    if (!file.type.startsWith('image/')) {
      alert("Only image files are allowed!");
      event.target.value = '';
      return;
    }
    this.registerForm.get('profilePhoto')?.setValue(file);
  }

  register() {

    if (this.registerForm.invalid) {
      this.registerForm.markAllAsTouched();
      return;
    }

    const formData = new FormData();
    formData.append('firstName', this.registerForm.value.firstName as string);
    formData.append('lastName', this.registerForm.value.lastName as string);
    formData.append('email', this.registerForm.value.email as string);
    formData.append('address', this.registerForm.value.address as string);
    formData.append('contact', this.registerForm.value.contact as string);
    formData.append('username', this.registerForm.value.username as string);
    formData.append('password', this.registerForm.value.password as string);
    formData.append('category', this.registerForm.value.category as string);

    const file = this.registerForm.value.profilePhoto;

    if (file instanceof File) {
      formData.append('profilePhoto', file);
    }

    this.loginAndRegister.register(formData, this.registerForm.value.category as string).subscribe({
      next: (response) => {
        localStorage.setItem("token", response.token);
        if (this.registerForm.value.category === "owner") {
          this.router.navigate(['/owner-home']);
        } else {
          this.router.navigate(['/renter-home']);
        }
      },
      error: (err) => {
        console.error(err);
        alert(err.error.message);
      }
    });

  }



}
