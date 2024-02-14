# Recipe Box

#### C Sharp MVC structure project using MySQl, EfCore, RestSharp, Microsoft Identity to create database with full CRUD and user roles

#### By Christopher Davila, Aaron Mejia, Zurisadai Gallegos

## Technologies Used

* _.NET 6.0.0_
* _MySQL_
* _Microsoft EF Core 6.0.0_
* _Microsoft AspNet Core Identity_
* _Newtonsoft.Json_
* _Pomelo EfCore for MySQL_
* _RestSharp_
* _Giphy Developers API_


## Description

This application is built into a C Sharp MVC framework and uses Microsoft EF Core's implicit building and directives to create a database code first via MySQL.  This database will track entries from the projects full CRUD functionality display entries based on specified queries in the controllers and views.  We also use RestSharp to make an API call to Giphy, and using the name property of our recipe database entires will make a query to Giphy, and then uses NewtonSoft to parse the JSON object response and display a food name related GIF on the detail page for a specific entry with its own unique ID.  Weve also used Microsofts EF Core Identity services to create a user registration and delegated specific roles for Users.  Admin has full CRUD functionality, a logged in has CRUD for Recipes but not categories, and non logged in users can simply view the recipes on the splash page.  A User should only have CRUD for there own entries created while logged in.

## Setup/Installation Requirements

<!-- Going forward, don't forget to include setup instructions in your README for an appsettings.json with a database connection string. -->

* _1. Clone this repo._
* _X. _dotnet add package MySqlConnector -v 2.2.0_
* _8. _create the file appsettings.json, and what code to include in it. We recommend using the above formatting and directing users to replace [YOUR-USERNAME-HERE] and [YOUR-PASSWORD-HERE] with the user's own user and password values. also add [YOUR-DB-NAME] with database used_
* _this format -> 
<!-- {
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Port=3306;database=[YOUR-DB-NAME];uid=[YOUR-USER-HERE];pwd=[YOUR-PASSWORD-HERE];"
  }
} -->
* _2. Open your terminal (e.g., Terminal or GitBash) and navigate to this project's production directory called "ProjectFile"._
* _3. In the command line, run the command `\$ dotnet run` to compile and execute the console application. Since this is a console application, you'll interact with it through text commands in your terminal._
* _4. Optionally, you can run `\$ dotnet build` to compile this console app without running it._
* _5. Use `\$ dotnet test run` in the Test directory to run test on the application_
* _6. use `\$ dotnet watch run` to cycle the server_
* _7. use `\$ dotnet watch run --launch-profile "production"` to run in production mode_


## Known Bugs

* _Any known issues_
* _should go here_

## License
[MIT](https://yourlicesnepage)
