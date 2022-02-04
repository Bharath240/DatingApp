using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{


    // [ApiController] //Attribute
    // [Route("api/[controller]")]
    public class UsersController : BaseApiController //Inheriting it from BaseApiController
    //As we Inherited it from BaseApiController, we no longer need to specify attributes [ApiController] and  [Route("api/[controller]")]
    {
        //We need to get the data from DataBase, for that we are going to use dependency injection 
        private readonly DataContext _context;
        public UsersController(DataContext context) {
            _context = context;
        }

        // Now we are going to add two endpoints
        // Endpoint 1 is to get all the users from the Database
        // Endpoint 2 is to get specific users from the Database


        //Endpoint 1
        [HttpGet] 
        public async Task<ActionResult<IEnumerable<AppUser>>> getUsers(){

            var users = await _context.Users.ToListAsync(); //Gettting Users Table data from Database

            //If you use the async keyword before a function definition, you can then use await within the function.
            // When you await a promise, the function is paused in a non-blocking way until the promise settles.
            // If the promise fulfills, you get the value back. If the promise rejects, the rejected value is thrown

            return  users;

        }


        //Endpoint 2
        //api/users/id
        [HttpGet("{id}")] 
        public async Task<ActionResult<AppUser>>  getUsers(int id)  {

            var user = await _context.Users.FindAsync(id); //Gettting specific user data based on their Id from Database

            return user;

        }
    }
}