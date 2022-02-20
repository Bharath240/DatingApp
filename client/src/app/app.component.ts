import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { User } from './_models/users';
import { AccountService } from './_services/account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'The Dating App';

  users : any;
  constructor(private accountService: AccountService){

  }


  //It is invoked only once when the directive is instantiated
  ngOnInit(){
  
    this.setCurrentUser();
  }




  //Method to check local storage of browser for checking a key 'user'
  setCurrentUser(){
    const user: User = JSON.parse(localStorage.getItem('user'));
    this.accountService.setCurrentUser(user);
  }
}
