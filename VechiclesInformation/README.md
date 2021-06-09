# VehiclesInformation

### How to run
1. Git clone this repo
2. In case there is an error while loading Nuget packages then restore the Nuget packages for the solution and update them if available
3. Compile and Run

### Web API Deploying on IIS
1. Install [.Net Core hosting bundle](https://docs.microsoft.com/en-us/aspnet/core/tutorials/publish-to-iis?view=aspnetcore-5.0&tabs=visual-studio)
2. Publish application ```dotnet publish --configuration Release``` or Publish to folder via Visual studio
3. Create Application pool with
    1. .Net clr version set to *No managed code*
    2. Identity set to user with write privilages
4. Create Website and select folder with published content
5. Navigate to http[s]://webserver[:port]/swagger/index.html


### Web Application Deploying on IIS
Rerequisite for deployement:
        Update the web config files for the below keys:
            <add key="UserName" value="" />
	    <add key="Password" value="" />
            <add key="ApiUrl" value="" />
        The UserName and Password should be available in UsersDetail table.
        The ApiUrl should have the port same as the web api url which is hosted on IIS. 
        There is a Databasefile "Vehicles" available in web api project which contains the sample data which 
        you have provided along with the user added in the User added in UserDetails table
        

1.Repeat the first 4 steps followed for web api deployment 
2.Skip step no 5
3.Browse the application.


# Project Description:
  This is a ASP.net web application using MVC architecture.
  Asp.net Web API uses SQLLite database and handles the backend logic. 

### Web Application:
There are 3 seperate views for the three controller methods displaying data as per the different requirements.
There is SyncStatus method which is getting called after every one minute to update the vehicle status in database. In order to view the latest status ,the page needs to be refreshed.

### Web API:
1.There are 3 seperate controllers for aech entity : Vehicles, Customer and Users.
2.There are different models for each controller method mapping to the database context.
3.The repositories are used to get or post the data to the database(sqlLite)
4.A seperate Integration exists for capturing the response of the status Sync from an external service. For the purpose of this project I have simulated the response for the vehicles, sending the vehicle connection status .
5.The Web API uses the Basic Authentication giving access to the api only to those users available in UserDetails table.

### Unit Test:
1.The solution has a unit test project using MSTest Framework and Moq .

### Database :
1.Used SQLLite for database.
2.There is a UserDetails table which contains username and password which will be used to access the api
3.There is CustomerDetails table which holds the customer information.
4.There is VehicleDetails table containing the vehicles details which is having Customerid as foreign key.

#Why this architecture?

1.This architecture is a way to separate and divide our large applications to smaller bits of pieces. 
2.Project uses Repository design pattern.
3.Separated classes/functions solve the problems of readability, modularity, and coupling, so does MVC. 
4.If we wanted to change a piece of code, we can tackle it in a smaller subset that is more or less isolated from the larger piece of code.
5.This allows us to add, modify or remove code more effeciently and logically. 
6.It also helps in testing, since similar code is sectioned into groups, we will be able to have better coverage of your tests cases.
7.Also very important is that we end up writing a lot less code.





