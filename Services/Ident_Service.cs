using Anastasia_SeniorProject.Domain;
using Anastasia_SeniorProject.mySettings;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Net.Mail;

namespace Anastasia_SeniorProject.Services
{
    public class Ident_Service : I_Ident_Service
    {
        private readonly UserManager <IdentityUser> myUserManager;
        private readonly JWTSettings myJwtSettings;

        public Ident_Service(UserManager<IdentityUser> userManager, JWTSettings jwtsettings)
        {
            myUserManager = userManager;
            myJwtSettings = jwtsettings;
        }

        private string SqlFun(string cmd)
        {
            SqlConnection myConnection = new SqlConnection("Data Source=tcp:anastasia-seniorprojectdbserver.database.windows.net,1433;Initial Catalog=Anastasia_SeniorProject_db;User Id=tasoakhv@anastasia-seniorprojectdbserver;Password=Test12345678.");
            SqlDataAdapter myAdapter;
            DataTable myResult = new DataTable();
            myConnection.Open();
            string varCmd = cmd;
            myAdapter = new SqlDataAdapter(varCmd, myConnection);
            myAdapter.Fill(myResult);
            string myId = "0";

            foreach (DataRow dr in myResult.Rows)
            {
                for (int i = 0; i < 1; i++)
                {
                    myId = dr.ItemArray[i].ToString();

                }
            }

            myConnection.Close();
            return myId;
        }

        private myAuthResult myAuthResult(IdentityUser newUser, string myCommand)
        {
            var myTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(myJwtSettings.secret);
            var myDescriptor = new SecurityTokenDescriptor
            {
                /* Properties */
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Email, newUser.Email),
                    new Claim("id", newUser.Id)

                }),

                Expires = DateTime.UtcNow.AddHours(6),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };


            var token = myTokenHandler.CreateToken(myDescriptor);
            var cmd = myCommand;
            string myId = SqlFun(cmd);

            return new myAuthResult
            {
                Successful = true,
                Token = myTokenHandler.WriteToken(token).ToString(),
                myUserId = myId

            };
        }

        public async Task<myAuthResult> Register_Service_Async(string email, string password)
        {
            string maxPrevID = SqlFun("Select max(Ident) from AspNetUsers");
            int myMaxId = Convert.ToInt32(maxPrevID);
            if ((myMaxId > 0) && (myMaxId < 100))
            {

                bool validEmail;
                try
                {
                    MailAddress myEmailAddress = new MailAddress(email);
                    validEmail = true;

                    var existingUser = await myUserManager.FindByEmailAsync(email);

                    if (existingUser != null)
                    {
                        return new myAuthResult
                        {
                            myError = new[] { "The user with this email address already exists" }

                        };
                    }

                    var newUser = new IdentityUser
                    {
                        Email = email,
                        UserName = email
                    };

                    var createdUser = await myUserManager.CreateAsync(newUser, password);

                    if (!createdUser.Succeeded)
                    {
                        return new myAuthResult
                        {
                            myError = createdUser.Errors.Select(x => x.Description)

                        };
                    }

                    return myAuthResult(newUser, "Select max(Ident) from AspNetUsers");


                }
                catch (FormatException)
                {
                    validEmail = false;
                    return new myAuthResult
                    {
                        myError = new[] { "Email address is not valid. Please enter a valid email address" }

                    };
                }
            }
            else 
            {
                return new myAuthResult
                {
                    myError = new[] { "Max user limit is reached" }

                };
            }
        }

        public async Task<myAuthResult> Login_Service_Async(string email, string password) 
        {
            var myUser = await myUserManager.FindByEmailAsync(email);
            if (myUser == null)
            {
                return new myAuthResult
                {
                    myError = new[] { "The user with this email adress does not exist" }

                };
            }

            var validPassword = await myUserManager.CheckPasswordAsync(myUser, password);
            if (!validPassword) 
            {
                return new myAuthResult
                {
                    myError = new[] { "Either email or password is incorrect." }

                };
            }

            string myCommand = "Select ident from AspNetUsers where Email = '" + email + "';";
            return myAuthResult(myUser, myCommand);
        }


    }
}


