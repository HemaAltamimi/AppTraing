import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Member } from '../_models/member';

@Injectable({
  providedIn: 'root'
})
export class MembersService {

  constructor(private http: HttpClient) { }

  getMembers(){
    return this.http.get<Member[]>(environment.apiUrl + 'users')
  }

  getMember(username:string){
    return this.http.get<Member>(environment.apiUrl + 'users/GetUserByUsername/'+username)
  }


}
