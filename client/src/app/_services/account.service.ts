import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ReplaySubject } from 'rxjs';
import { map } from 'rxjs/operators';
import { User } from '../_models/users';

@Injectable({
  providedIn: 'root'
})

//Services are used to request API
export class AccountService {
  baseUrl = "https://localhost:5001/api/";

  //ReplaySubject has an internal buffer that will store a specified number of values that it has observed
  private currentUserSource = new ReplaySubject<User>(1); //We provided 1 in ReplaySubject, As we want to store one user details 
  currentUser$ = this.currentUserSource.asObservable()

  constructor(private http: HttpClient) { 

  }


  //Creating a login method. This will receive the inputs from the input fields in NavigationBar
  login(model : any) {
    return this.http.post(this.baseUrl + "account/login", model).pipe ( 
  // Pipes are simple functions to use in template expressions to accept an input value and return a transformed value
      map((response: User) => {
        const user = response; //storing the response into the user
        if(user) {
          localStorage.setItem('user', JSON.stringify(user))
          //We are mapping the user with string user and storing it in localStorage
          this.currentUserSource.next(user); 
        }
      })
    );
  }

  logout() {
    localStorage.removeItem('user');
    //removing user from the local storage on logout

    this.currentUserSource.next(null); 
  }

  setCurrentUser(user : User){
    this.currentUserSource.next(user); 
  }

  
  register(model : any){
    return this.http.post(this.baseUrl + "account/register", model).pipe(
      map((user : User) => {
        if(user) {
          localStorage.setItem('user', JSON.stringify(user))
          //We are mapping the user with string user and storing it in localStorage
          this.currentUserSource.next(user); 
        }
        return "User has been registered successfully!!!"
      })
    )
  }
}




