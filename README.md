# HikeApp-API

This file aims to provide information regarding HikeApp API as well as database. HikeApp API represents an application programming interface that provides a set of services allowing HikeApp mobile application to make API calls and utilize the provided services. HikeApp API was developed using C# programming language. Application programming interface provides following services: sign up, sign in, search user based on the email address, search user based on the id as well as search every user stored in the database.

Now that all of the services are listed, they may be discussed separately. 
The task of user registration is of return type myAuthResult which means that it returns an instance of myAuthResult class. Moreover, the service responsible for the sign up process utilizes following request URL: https://anastasiaseniorprojecthikeapp.azurewebsites.net/api/v1/identity/register. The flow is as follows; a POST request is sent when implementing user registration service. Moreover, two parameters have to be passed within the request body. These parameters are: email address and password. At this point it is possible to register only 100 users. Because of this, the program firstly checks if the maximum number of the existing users is less than 100 and then moves on to the next step. If the number of the existing users is less than 100, then it is possible to register a new user and the program checks if the passed email is valid. If the email is valid, then the program moves to the next precondition and checks if the user with such email address is already stored in the database. If not, then the user registration was successful and user data is stored in the table AspNetUsers. As mentioned above, the return type of the task is myAuthResult. The object of type myAuthResult contains the following information: the fact that the action was successful, token (JWT) and user id. More precisely, the instance of class myAuthResult is created with explicit value constructor with two parameters: instance of class IdentityUser as well as a string representing a SQL query. The explicit value constructor makes a SqlFun function call and passes the query to SqpFun method which retrieves the user id. Please refer to screenshots 1 and 2 in Appendix A in order to view the illustration of user registration request as well as response. 

Now that the user registration is already discussed, service responsible for log in may be considered. This service implements following request URL:
https://anastasiaseniorprojecthikeapp.azurewebsites.net/api/v1/identity/login. A POST request is sent when utilizing this service. The request body needs two parameters: email and password. Firstly, the program checks if the user with such email is already stored in the database. If there is no such user registered, the program returns an appropriate error message indicating that no such user is registered. Otherwise, the program checks if the passed password is valid. If not, the program returns an error message indicating that this combination of email and password is not valid. Otherwise, the log in is successful and a token (JWT) along with the user id is being returned. The return type of the task responsible for user log in is myAuthResult. This means that the information that is being returned by this task is contained by an instance of class myAuthResult.
The flow is the same as it was in case of user registration service. The object of myAuthResult is instantiated with explicit value constructor accepting two parameters: an instance of class IdentityUser as well as a string representing a SQL query. As mentioned above, explicit value constructor calls the method SqlFun that retrieves the results based on the passed query. 
Please refer to screenshots 3 and 4 in Appendix A in order to view the illustration of the service.

Now that the user registration as well as log in services are already considered, the services responsible for different kinds of searches may be discussed. All of the services responsible for searching utilize private method SqlFunGet. The method SqlFunGet accepts a string parameter which represents a SQL query and returns the results of the select commands. 

One of the services is responsible for returning email and id of all the users stored within the database. In this case a GET request is being sent with the following request URL: https://anastasiaseniorprojecthikeapp.azurewebsites.net/api/v1/identity/GetAllUsers. The service returns a json object containing the data of every user that has been registered. Please refer to screenshot 5 in Appendix A in order to view the illustration of the service response.
 
The next service that is utilized for searching purposes looks for the users based on their email addresses. The request URL utilized for this service is:
https://anastasiaseniorprojecthikeapp.azurewebsites.net/api/v1/identity/GetUserByEmail.
In this case, a POST request is sent with just one string parameter passed by the request body. The request returns a json object containing the data (email address and id) of all the users whose email addresses contain the string that was passed in the request body. In case no email contains the string indicated in the request body “N/A” will be returned. Please refer to screenshots 6 and 7 in Appendix A in order to view the resulting display of service execution.

The next request is very similar to the previous one. The difference is that the id is passed in the request body and corresponding user data – email address and id is being returned. Please refer to the URL below in order to view the request URL corresponding to this service. https://anastasiaseniorprojecthikeapp.azurewebsites.net/api/v1/identity/GetUserById. Please refer to screenshots 8 and 9 in Appendix A in order to view the resulting display of service execution.

Last but not least, HikeApp API utilizes Swagger for documentation purposes. Swagger configuration allows developers to organize the services in one separate area. Moreover, it enables developers as well as testers to test the services without external software such as Postman or Insomnia. This results in an easier and faster debugging process. 
Please refer to the following URL in order to access Swagger for HikeApp API:
https://anastasiaseniorprojecthikeapp.azurewebsites.net/swagger/index.html.

HikeApp Database

Now that HikeApp API is already considered, the corresponding database may be discussed. As it is known, ASP.NET Core provides a framework in order to store as well as manage user accounts in ASP.NET Core applications (Vickers, 2019). When creating the project, individual user accounts were chosen as the authentication tools. Because of this Identity was added to the project automatically. What’s more, Identity uses Entity Framework data model (Vickers, 2019). Because of this, when running the migrations, the database was created with the following tables: AspNetRoles, AspNetUserClaims, AspNetUserLogins, AspNetUserRoles, AspNetUsers as well as AspNetUserTokens. Please refer to Figure 1 below in order to view the structure of the database.

Figure 1
![Figure 1](https://github.com/anastasiaakhv/HikeApp-API/blob/main/Figure%201.png)








However, the program only implements the table AspNetUsers. As mentioned, the user information is stored in the table AspNetUsers. Please refer to Figure 2 in order to view the fields contained by the table AspNetUsers. Not every field is used. However, they remain in the table because of the possible future development of the project.

Figure 2
![Figure 2](https://github.com/anastasiaakhv/HikeApp-API/blob/main/Figure%202.png)
 

All of the fields were created automatically when running the migrations except the field ident which is of type int and was declared with identity characteristic by an additional SQL query. The reason is that the original id field (created automatically) was too large of a size and could not be included in the packet. Therefore, the field ident of type int with smaller size was added to the table and was made an identity. More precisely, it increments by one with every new user registration – meaning that each new user has a unique id that can be used for the identification purposes. Last but not least, the entire project was deployed to Azure so that the HikeApp mobile application could interact with the API and make API calls. 


References:
Vickers, A. (2019, January 07). Identity model customization in ASP.NET Core. Retrieved from https://docs.microsoft.com/en-us/aspnet/core/security/authentication/customize-identity-model?view=aspnetcore-5.0

Appendix A
Screenshot 1
![Screenshot 1](https://github.com/anastasiaakhv/HikeApp-API/blob/main/Scr1.png)
 
Screenshot 2 
![Screenshot 2](https://github.com/anastasiaakhv/HikeApp-API/blob/main/scr2.png)
 
Screenshot 3
![Screenshot 3](https://github.com/anastasiaakhv/HikeApp-API/blob/main/Scr3.png)
 
Screenshot 4
![Screenshot 4](https://github.com/anastasiaakhv/HikeApp-API/blob/main/Scr4.png)
 
Screenshot 5 
![Screenshot 5](https://github.com/anastasiaakhv/HikeApp-API/blob/main/Scr5.png)
 
Screenshot 6
![Screenshot 6](https://github.com/anastasiaakhv/HikeApp-API/blob/main/Scr6.png)

Screenshot 7
![Screenshot 7](https://github.com/anastasiaakhv/HikeApp-API/blob/main/Scr7.png)

Screenshot 8
![Screenshot 8](https://github.com/anastasiaakhv/HikeApp-API/blob/main/Scr8.png)

Screenshot 9
![Screenshot 9](https://github.com/anastasiaakhv/HikeApp-API/blob/main/Scr9.png)
 


