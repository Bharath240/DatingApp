import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'The Dating App';

  users : any;
  constructor(private http : HttpClient){

  }


  //It is invoked only once when the directive is instantiated
  ngOnInit(){
    this.getUsers();
  }


  //method to get users data from GET API
  getUsers(){
    this.http.get("https://localhost:5001/api/users").subscribe(response => {
      this.users = response
    }, error =>{
      console.log(error)
    })  
    //Constructs a GET request that interprets the body as a JSON object 
    //and returns the response body as a JSON object.
  }
}
