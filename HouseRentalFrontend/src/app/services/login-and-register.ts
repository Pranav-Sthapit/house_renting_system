import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { LoginDTO } from '../../types/loginTypes';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class LoginAndRegister {

  private BASE_URL = environment.apiUrl + "/api";

  constructor(private httpClient: HttpClient) { }

  login(data: LoginDTO, category: string): Observable<any> {
    return this.httpClient.post(this.BASE_URL + `/LoginRegister/login/${category}`, data);
  }

  register(data: FormData, category: string): Observable<any> {
    return this.httpClient.post(this.BASE_URL + `/LoginRegister/register/${category}`, data);
  }

}
