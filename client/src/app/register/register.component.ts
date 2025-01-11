import { Component, EventEmitter, inject, input, output, Output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AccountService } from '../_services/account.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [FormsModule,],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
  accountService = inject(AccountService);
  toastr = inject(ToastrService);
  userFromHomeComponent = input.required<any>();
  cancelRegister = output<boolean>();
 // @Input() userFromHomeComponent : any;
  //@Output() cancelRegister = new EventEmitter()
  model: any={};

  register() {
    this.accountService.register(this.model).subscribe(
      {
        next : _ =>{
          this.cancel();
        },
        complete : () => this.toastr.success("Registration successful"),
        error : error => this.toastr.error(error.error)
      }
    )
  }

  cancel() {
    this.cancelRegister.emit(false)
  }


}
