import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';

@Component({
  selector: 'app-test-error',
  templateUrl: './test-error.component.html',
  styleUrls: ['./test-error.component.css']
})
export class TestErrorComponent {
  baseUrl = "http://localhost:5211/api/";

  validations:string[]=[];

  constructor(private httpClient:HttpClient) {

  }

  get404Error(){
    this.httpClient.get(this.baseUrl+'Mothana/not-found').subscribe({
      next: res => console.log(res),
      error: res => console.log(res),
    })
  }

  get400Error(){
    this.httpClient.get(this.baseUrl+'Mothana/bad-request').subscribe({
      next: res => console.log(res),
      error: res => console.log(res),
    })
  }
  get401Error(){
    this.httpClient.get(this.baseUrl+'Mothana/auth').subscribe({
      next: res => console.log(res),
      error: res => console.log(res),
    })
  }
  get500Error(){
    this.httpClient.get(this.baseUrl+'Mothana/server-error').subscribe({
      next: res => console.log(res),
      error: res => console.log(res),
    })
  }


  get400ValidationError(){
    this.httpClient.post(this.baseUrl+'account/register',{}).subscribe({
      next: res => console.log(res),
      error: res => {console.log(res); this.validations=res},
    })
  }
}
