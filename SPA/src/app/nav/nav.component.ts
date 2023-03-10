import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model:any={}
  constructor(public accountService:AccountService, private router:Router,private toastr: ToastrService) {
  }

  ngOnInit(): void {
    this.accountService.currentUser$.subscribe({
      next: response =>{
        this.model.username=response?.username
        console.log(response)
      },
      error: err=>{console.log(err)}
    })
  }

  login(){
    this.accountService.login(this.model).subscribe({
      next: response =>{
        this.router.navigate(["members"]);
      }
    })
  }

  logout(){
    this.accountService.logout();
    this.router.navigate([""]);
  }

}
