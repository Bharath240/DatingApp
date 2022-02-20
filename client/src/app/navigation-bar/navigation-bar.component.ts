import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { User } from '../_models/users';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-navigation-bar',
  templateUrl: './navigation-bar.component.html',
  styleUrls: ['./navigation-bar.component.css']
})
export class NavigationBarComponent implements OnInit {

  model : any ={}
  loggedIn : boolean


  constructor(public accountService: AccountService) { //Injecting AccountService 

   }

  ngOnInit(): void {
  
  }

  login() {

    //passing our model(username and password) to login method of AccountService
    //.subscribe will return a response (data) from the api
    this.accountService.login(this.model).subscribe(response => {
      console.log(response);
     
    }, error => {
      console.log(error);
    })
  }

  logout() {
    
    this.accountService.logout();
  }

  

}




