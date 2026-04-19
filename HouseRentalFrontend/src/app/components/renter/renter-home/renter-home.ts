import { Component } from '@angular/core';
import { RouterOutlet } from "@angular/router";
import { RenterNavBar } from "../renter-nav-bar/renter-nav-bar";

@Component({
  selector: 'app-renter-home',
  imports: [RouterOutlet, RenterNavBar],
  templateUrl: './renter-home.html',
  styleUrl: './renter-home.css',
})
export class RenterHome {

}
