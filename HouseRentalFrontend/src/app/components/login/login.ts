import { Component } from '@angular/core';
import { LoginDTO } from '../../../types/loginTypes';
import { FormsModule } from '@angular/forms';
import { validateUsername, validatePassword } from '../../../validations/personValidations';
import { NgIf } from '@angular/common';
import { Router, RouterLink } from '@angular/router';
import { LoginAndRegister } from '../../services/login-and-register';
@Component({
  selector: 'app-login',
  imports: [FormsModule, NgIf, RouterLink],
  templateUrl: './login.html',
  styleUrl: './login.css',
})
export class Login {

  loginDTO: LoginDTO = {
    username: '',
    password: ''
  }
  category = 'renter';
  usernameError: string | null = null;
  passwordError: string | null = null;
  loginError: string = "Invalid Username of password";
  showLoginError: boolean = false;

  constructor(private loginAndRegister: LoginAndRegister, private router: Router) { }

  validateUsername() {
    this.usernameError = validateUsername(this.loginDTO.username);
  }

  validatePassword() {
    this.passwordError = validatePassword(this.loginDTO.password);
  }

  login() {
    this.validateUsername();
    this.validatePassword();
    if (this.usernameError == null && this.passwordError == null) {

      this.loginAndRegister.login(this.loginDTO, this.category).subscribe({
        next: (response) => {
          localStorage.setItem("token", response.token);
          if (this.category === "owner") {
            this.router.navigate(['/owner-home']);
          } else {
            this.router.navigate(['/renter-home']);
          }
        },
        error: (error) => {
          console.error(error);
          alert("Invalid email or password");
        }
      });
    }
  }

}
