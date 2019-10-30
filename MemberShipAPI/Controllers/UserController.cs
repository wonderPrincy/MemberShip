using MemberShipAPI.Library;
using MemberShipAPI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Security;

namespace MemberShipAPI.Controllers
{
    [RoutePrefix("api/user")]

    public class UserController : ApiController
    {
        private UserService userService;

        public UserController()
        {
            userService = new UserService();
        }


        [HttpPost]
        [Route("createuser")]
        public string CreateUser(User user)
        {
            var existUser = userService.GetUser(user.UserName);
            if (existUser == null)
            {
                var status = userService.Create(user);
                string createdStatus = string.Empty;
                switch (status)
                {
                    case MembershipCreateStatus.Success:
                        createdStatus = "The user account was successfully created";

                        break;
                    // This Case Occured whenver we send duplicate UserName  
                    case MembershipCreateStatus.DuplicateUserName:
                        createdStatus = "The user with the same UserName already exists!";
                        break;
                    //This Case Occured whenver we give duplicate mail id  
                    case MembershipCreateStatus.DuplicateEmail:

                        createdStatus = "The user with the same email address already exists!";
                        break;
                    //This Case Occured whenver we send invalid mail format  
                    case MembershipCreateStatus.InvalidEmail:
                        createdStatus = "The email address you provided is invalid.";
                        break;
                    //This Case Occured whenver we send empty data or Invalid Data  
                    case MembershipCreateStatus.InvalidAnswer:
                        createdStatus = "The security answer was invalid.";
                        break;
                    // This Case Occured whenver we supplied weak password  
                    case MembershipCreateStatus.InvalidPassword:
                        createdStatus = "The password you provided is invalid. It must be 7 characters long and have at least 1 special character.";
                        break;
                    default:
                        createdStatus = "There was an unknown error; the user account was NOT created.";
                        break;
                }
                return JsonConvert.SerializeObject(new { createdStatus }, Formatting.None,
                               new JsonSerializerSettings()
                               {
                                   ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                               });
            }
            else
            {
                existUser.Email = user.Email;
                existUser.IsApproved = user.IsApproved;
                userService.Update(existUser);
                return JsonConvert.SerializeObject("The user account was successfully updated.", Formatting.None,
                             new JsonSerializerSettings()
                             {
                                 ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                             });
            }
        }

        [HttpPost]
        public string DeleteUser(string userName)
        {
            bool isDeleted = userService.Delete(userName);
            if (isDeleted)
            {
                return JsonConvert.SerializeObject("The user account was successfully deleted.", Formatting.None,
                            new JsonSerializerSettings()
                            {
                                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                            });
            }
            else
            {
                return JsonConvert.SerializeObject("There is any error occured while deleting user.", Formatting.None,
                            new JsonSerializerSettings()
                            {
                                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                            });
            }
        }
        [Route("login")]
        [HttpGet]      
        public string Login(Login login)
        {
            string token = string.Empty;
           
            if (!string.IsNullOrEmpty(login.UserName) && !string.IsNullOrEmpty(login.Password))
            {
                token = userService.ValidateUser(login);
                if (!string.IsNullOrEmpty(token))

                    return JsonConvert.SerializeObject(token, Formatting.None,
                               new JsonSerializerSettings()
                               {
                                   ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                               });
                else
                    return JsonConvert.SerializeObject("Invalid username or Password.", Formatting.None,
                               new JsonSerializerSettings()
                               {
                                   ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                               });
            }
            else
            {
                return JsonConvert.SerializeObject("Please enter both fields username or password.", Formatting.None,
                           new JsonSerializerSettings()
                           {
                               ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                           });
            }

        }
        [HttpPost]
        public string ForgotPssword(string username, string securityAnswer)
        {
            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(securityAnswer))
            {
                var existuser = userService.GetUser(username);
                if(existuser!=null)
                {
                    string newPassword = userService.ResetPassword(existuser, securityAnswer);
                    return JsonConvert.SerializeObject(newPassword, Formatting.None,
                                   new JsonSerializerSettings()
                                   {
                                       ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                   });
                }
                else
                    return JsonConvert.SerializeObject("Username does not exist", Formatting.None,
                                   new JsonSerializerSettings()
                                   {
                                       ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                   });

            }
            else
            {
                return JsonConvert.SerializeObject("Please enter both fields username and securityAnswer.", Formatting.None,
                           new JsonSerializerSettings()
                           {
                               ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                           });
            }
        }
    }
}
