import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { LoginDTO } from '../../types/loginTypes';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class LoginAndRegister {

  private BASE_URL = "http://localhost:5064/api";

  constructor(private httpClient: HttpClient) { }

  login(data: LoginDTO, category: string): Observable<any> {
    return this.httpClient.post(this.BASE_URL + `/LoginRegister/login/${category}`, data);
  }

  register(data:FormData,category:string): Observable<any> {
    return this.httpClient.post(this.BASE_URL + `/LoginRegister/register/${category}`, data);
  }

}
