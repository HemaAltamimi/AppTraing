import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-server-error',
  templateUrl: './server-error.component.html',
  styleUrls: ['./server-error.component.css']
})
export class ServerErrorComponent {
 error:any


 constructor(private router:Router) {
   const navg =this.router.getCurrentNavigation();

   this.error= navg?.extras?.state?.['error'];
 }
}
