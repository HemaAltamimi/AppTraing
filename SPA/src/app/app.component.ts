import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'SPA';
  users : any;
  constructor(private http:HttpClient) {

  }
  ngOnInit(): void {
     this.http.get("http://localhost:5211/api/Users").subscribe(res => {
      this.users =res;
      console.log("next")
     },
     err => {console.log("error")},
     () => {console.log("Complete")}
     )
  }
}
