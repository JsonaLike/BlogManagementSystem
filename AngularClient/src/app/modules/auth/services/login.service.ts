import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class LoginService {
  private readonly apiUrl = environment.baseApiUrl + '/account';
  constructor(private http:HttpClient) { }
  login(loginView:any){
    return this.http.post<any>(this.apiUrl+'/login', loginView,{withCredentials:true});
  }
}
