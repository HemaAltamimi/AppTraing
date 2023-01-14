import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { map, Observable } from 'rxjs';
import { User } from '../_models/user';
import { AccountService } from '../_services/account.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(private router:Router,private accountService:AccountService) {

  }
  canActivate(): Observable<boolean>{
      return this.accountService.currentUser$.pipe(
        map((user:any) => {
          if(user) return true
          else{
            this.router.navigate([""])
            return false
          }
        })
      )
  }

}
