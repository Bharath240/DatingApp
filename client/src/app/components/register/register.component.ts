import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AccountService } from 'src/app/_services/account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  //To receive data from parent component
  @Input() usersFromHomeComponent : any;

  //To send data to parent component
  @Output() cancelRegister = new EventEmitter();

  model : any = {}

  constructor(private accountService: AccountService) { }

  ngOnInit(): void {
  }


  //Method to register a User
  register(){
    this.accountService.register(this.model).subscribe(response => {
      console.log(response)
      this.cancel()
    }, error => {
      console.log(error)
    });
  }

  cancel(){
    this.cancelRegister.emit(false);
  }
}
