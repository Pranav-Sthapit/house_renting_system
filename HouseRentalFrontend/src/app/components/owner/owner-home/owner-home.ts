import { Component } from '@angular/core';
import { OwnerNavbar } from "../owner-navbar/owner-navbar";
import {AddProperty} from "../add-property/add-property";
import {ViewProperty} from "../view-property/view-property";
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-owner-home',
  imports: [OwnerNavbar,RouterOutlet],
  templateUrl: './owner-home.html',
  styleUrl: './owner-home.css',
})
export class OwnerHome {

}
