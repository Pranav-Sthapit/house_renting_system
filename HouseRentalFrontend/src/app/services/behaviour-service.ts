import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class BehaviourService {

  private BASE_URL = environment.apiUrl + "/api";

  constructor(private httpClient: HttpClient) { }

  incrementViewCount(propertyId: number){
    const token = localStorage.getItem('token');

    const headers = new HttpHeaders({
      Authorization: `Bearer ${token}`
    });

    return this.httpClient.put(this.BASE_URL + `/Behaviour/view/${propertyId}`, {}, { headers });
  }

  applyForProperty(propertyId: number){
    const token = localStorage.getItem('token');

    const headers = new HttpHeaders({
      Authorization: `Bearer ${token}`
    });

    return this.httpClient.put(this.BASE_URL + `/Behaviour/apply/${propertyId}`, {}, { headers });
  }
}
