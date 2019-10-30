# MemberShip
create, edit, update and delete functionality for users using asp.net membership database

Installed nuget:-System.IdentityModel.Tokens.Jwt for generate token

For Create or Update User :-
EndPoint:- http://localhost:64165/api/user/CreateUser

Request:-
{
	"UserName":"princy",
	"Password":"123456#",
	"Email":"wonderprincy@gmail.com",
	"SecurityQuestion":"Favourite  Actor",
    "SecurityAnswer":"Akshay",
    "IsApproved":true
}
Above all fields are required except isapproved.
if username exist in database it will update Email and IsApproved status in database. Otherwise it will create new User.

Authenticate User:-
EndPoint:- http://localhost:64165/api/user/login
request:-
{
	"UserName":"princy",
	"Password":"123456#"
}

It will provide Token in response.

Delete User:-
Endpoint:-http://localhost:64165/api/user/DeleteUser?username=

username field is require to delete user

Forgot password:-
EndPoint:- http://localhost:64165/api/user/ForgotPassword?username=princy&securityAnswer

username and securityAnswer both fields are required to Fprgot password. It will provide you auto generated password
