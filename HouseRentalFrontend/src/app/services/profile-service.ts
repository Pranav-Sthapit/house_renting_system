import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class ProfileService {

  private baseUrl = 'http://localhost:5064/api';

  constructor(private http: HttpClient) { }

  getOwnerProfile() {
    const token = localStorage.getItem('token');

    const headers = new HttpHeaders({
      Authorization: `Bearer ${token}`
    });
    return this.http.get(`${this.baseUrl}/Owner/`, { headers });
  }

  updateOwnerInfo(formData: FormData) {
    const token = localStorage.getItem('token');

    const headers = new HttpHeaders({
      Authorization: `Bearer ${token}`
    });
    return this.http.put(`${this.baseUrl}/Owner/`, formData, { headers });
  }

  getRenterProfile(){
    const token = localStorage.getItem('token');

    const headers = new HttpHeaders({
      Authorization: `Bearer ${token}`
    });
    return this.http.get(`${this.baseUrl}/Renter/`, { headers });
  }

  updateRenterInfo(formData:FormData){
    const token = localStorage.getItem('token');

    const headers = new HttpHeaders({
      Authorization: `Bearer ${token}`
    });
    return this.http.put(`${this.baseUrl}/Renter/`, formData, { headers });
  }

}
