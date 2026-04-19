import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class RentalService {

  private baseUrl = 'http://localhost:5064/api';

  constructor(private http: HttpClient) { }

  getRenters(propertyId: number) {
    const token = localStorage.getItem('token');

    const headers = new HttpHeaders({
      Authorization: `Bearer ${token}`
    });
    return this.http.get(`${this.baseUrl}/Rental/owner/${propertyId}`, { headers });
  }

  acceptRenter(propertyId: number, renterId: number) {
    const token = localStorage.getItem('token');

    const headers = new HttpHeaders({
      Authorization: `Bearer ${token}`
    });
    return this.http.put(`${this.baseUrl}/Rental/owner/${propertyId}/${renterId}/approve`, {}, { headers });
  }

  rejectRenter(propertyId: number, renterId: number) {
    const token = localStorage.getItem('token');

    const headers = new HttpHeaders({
      Authorization: `Bearer ${token}`
    });
    return this.http.put(`${this.baseUrl}/Rental/owner/${propertyId}/${renterId}/reject`, {}, { headers });
  }

  getRenterDetails(propertyId:number,renterId:number){
    const token = localStorage.getItem('token');

    const headers = new HttpHeaders({
      Authorization: `Bearer ${token}`
    });
    return this.http.get(`${this.baseUrl}/Rental/owner/${propertyId}/${renterId}`, { headers });
  }



  getRentalsOfRenter(){
    const token = localStorage.getItem('token');

    const headers = new HttpHeaders({
      Authorization: `Bearer ${token}`
    });
    return this.http.get(`${this.baseUrl}/Rental/renter`, { headers });
  }

  getRentalAndPropertyDetails(propertyId:number){
    const token = localStorage.getItem('token');

    const headers = new HttpHeaders({
      Authorization: `Bearer ${token}`
    });
    return this.http.get(`${this.baseUrl}/Rental/renter/${propertyId}`, { headers });
  }

  addRental(propertyId:number,tenant:string,rent:number){
    const token = localStorage.getItem('token');

    const headers = new HttpHeaders({
      Authorization: `Bearer ${token}`
    });
    return this.http.post(`${this.baseUrl}/Rental/renter/${propertyId}`, {tenant,rent}, { headers });
  }

  updateRental(propertyId:number,tenant:string,rent:number){
    const token = localStorage.getItem('token');

    const headers = new HttpHeaders({
      Authorization: `Bearer ${token}`
    });
    return this.http.put(`${this.baseUrl}/Rental/renter/${propertyId}`, {tenant,rent}, { headers });
  }

  deleteRental(propertyId:number){
    const token = localStorage.getItem('token');

    const headers = new HttpHeaders({
      Authorization: `Bearer ${token}`
    });
    return this.http.delete(`${this.baseUrl}/Rental/renter/${propertyId}`, { headers });
  }


}
