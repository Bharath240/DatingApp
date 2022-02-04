using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AccountController :  BaseApiController //Inheriting it from BaseApiController
    {
        //We need to get the data from DataBase, for that we are going to use dependency injection 
         private readonly DataContext _context;
        private readonly ITokenInterface _tokenInterface;
        public AccountController(DataContext context, ITokenInterface tokenInterface) {
            _tokenInterface = tokenInterface;
            _context = context;
        }



        // method to register user
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto){ //Using Data Transfer Object(DTO) as a parameter

            if(await UserExists(registerDto.UserName)){ //If Username is not unique, if condition executes and return Bad request and stops the execute of the further code
                return BadRequest("Username is taken");

            }
            using var hmac= new HMACSHA512(); //This will provide us the Hashing Algorithm, for storing password

            var user = new AppUser{
                UserName = registerDto.UserName,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                PasswordSalt = hmac.Key
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync(); //Calls the database and saves/adds the users information

            return new UserDto
            {
                UserName = user.UserName,
                Token = _tokenInterface.CreateToken(user)
            };
    }


    //Method to keep our username unique
    private async Task<bool> UserExists(string username){

        return await _context.Users.AnyAsync( data => data.UserName.ToLower() == username.ToLower());
        //The above statement checks, whether the Username is already exists in our database or not
        //If it exists, it will return true and vice versa
    }


    //method to login user
    [HttpPost("login")]

    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto){
        
       
        var user = await _context.Users.SingleOrDefaultAsync(data => data.UserName == loginDto.UserName); 
       //If the username in database matches with the username entered by user, then that particular user record will stored in user.
        //If the usernames doesn't match null is stored in user

        if(user == null) {
            return Unauthorized("Invalid Username");
        }

        var hmac = new HMACSHA512(user.PasswordSalt);

        var ComputeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

        for(int i=0; i < ComputeHash.Length;i++) {

            if(ComputeHash[i] != user.PasswordHash[i]){
             return Unauthorized("Invalid Password");
            } 
        }

        return new UserDto
            {
                UserName = user.UserName,
                Token = _tokenInterface.CreateToken(user)
            };;

    }
}
}