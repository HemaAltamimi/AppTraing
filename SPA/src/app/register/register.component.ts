import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
 model:any={};

 @Output() cancel = new EventEmitter();

constructor(private accoutSerive:AccountService, private toastr:ToastrService) {

}

 ngOnInit(): void {

 }

 Register(){
  this.accoutSerive.register(this.model).subscribe({
    next:() =>{
      this.Cancel();
    }
  })
 }

 Cancel(){
this.cancel.emit(false);
 }
}
