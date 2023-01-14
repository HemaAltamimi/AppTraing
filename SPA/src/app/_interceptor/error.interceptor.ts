import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse
} from '@angular/common/http';
import { catchError, Observable } from 'rxjs';
import { ToastrService } from 'ngx-toastr';
import { NavigationExtras, Router } from '@angular/router';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {

  constructor(private toastr:ToastrService,private router:Router) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    return next.handle(request).pipe(
      catchError((error:HttpErrorResponse) =>{
         if(error){
          switch(error.status){
            case 400:
              if(error.error.errors){
                for(const key in error.error.errors){
                  if(error.error.errors[key])
                  this.toastr.error(error.error.errors[key],error.status.toString())
                }

              }
              else{
                this.toastr.error(error.error,error.status.toString())
              }
              break;
            case 401:
              this.toastr.error("Unauthraized",error.status.toString());
              break;

            case 404:
              this.router.navigateByUrl('/not-found')
              break;

            case 500:
              const exceptions:NavigationExtras={state:{error:error.error}}
               this.router.navigateByUrl('/server-error',exceptions)
              break;
            default:
              this.toastr.error("Something unexpected went worng");
              break;
          }
         }
         throw error;
      })
    );
  }
}
