import { Component, EventEmitter, inject, input, output, Output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [FormsModule,],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
  accountService = inject(AccountService);
  userFromHomeComponent = input.required<any>();
  cancelRegister = output<boolean>();
 // @Input() userFromHomeComponent : any;
  //@Output() cancelRegister = new EventEmitter()
  model: any={};

  register() {
    this.accountService.register(this.model).subscribe(
      {
        next : response =>{
          console.log(response)
          this.cancel();
        },
        complete : () => console.log("Success")
      }
    )
  }

  cancel() {
    this.cancelRegister.emit(false)
  }


}
