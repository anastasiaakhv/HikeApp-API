using Anastasia_SeniorProject.Contracts;
using Anastasia_SeniorProject.Contracts.V1;
using Anastasia_SeniorProject.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;



namespace Anastasia_SeniorProject.Controllers.V1
{
    public class IdentUserController : Controller
    {
        private readonly I_Ident_Service identService;
        public IdentUserController(I_Ident_Service identityService) 
        {
            identService = identityService;
        }

        [HttpGet(myRoutes.Identity.GetAllUsers)]
        public IActionResult GetAllUsers()
        {
            string myCmd = "Select Email, Ident from AspNetUsers";
            string myResults = SqlFunGet(myCmd);
            if (myResults == null)
            {
                return Ok("There are no users registered");
            }
            else 
            {
                return Ok(myResults);
            }
            
        }

        [HttpPost(myRoutes.Identity.GetUserByEmail)]
        public IActionResult GetUserByEmail([FromBody] GetUserbyEmailRequest request)
        {
            if (!(request == null))
            {
                string myKeyword = request.myKeyword;
                string myCmd = "Select Email, Ident from AspNetUsers where Email like '%" + myKeyword + "%';";
                string myResults = SqlFunGet(myCmd);
                if (myResults == null)
                {
                    return Ok("N/A");
                }
                else
                {
                    return Ok(myResults);
                }
            }
            else 
            {
                return BadRequest("Wrong request - Please enter a your search keyword");
            }
           
        }

        [HttpPost(myRoutes.Identity.GetUserById)]
        public IActionResult GetUserByID([FromBody] GetUserByIdRequest request)
        {
            if (!(request == null))
            {
                string myUserId = request.myId;
                string myCmd = "Select Email, Ident from AspNetUsers where ident = " + myUserId + ";";
                string myResults = SqlFunGet(myCmd);
                if (myResults == null)
                {
                    return Ok("N/A");
                }
                else
                {
                    return Ok(myResults);
                }
            }
            else 
            {
                return BadRequest("Wrong request -Please enter a your search id");
            }
           
        }

        [HttpPost(myRoutes.Identity.Register)]
        public async Task<IActionResult> Register([FromBody] UserRegRequest request) 
        {
            if (!(request == null))
            {
                var generalAuthResp = await identService.Register_Service_Async(request.Email, request.Password);
                if (!generalAuthResp.Successful)
                {
                    return BadRequest(new Auth_f_Resp
                    {
                        myError = generalAuthResp.myError
                    });

                }
                return Ok(new Auth_s_Resp
                {
                    Token = generalAuthResp.Token,
                    myUserId = generalAuthResp.myUserId
                });
            }
            else 
            {
                return BadRequest("Wrong request - Please provide all of the parameters");
            }
            
        }

        [HttpPost(myRoutes.Identity.Login)]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (!(request == null))
            {
                var generalAuthResp = await identService.Login_Service_Async(request.Email, request.Password);
                if (!generalAuthResp.Successful)
                {
                    return BadRequest(new Auth_f_Resp
                    {
                        myError = generalAuthResp.myError
                    });

                }
                return Ok(new Auth_s_Resp
                {
                    Token = generalAuthResp.Token,
                    myUserId = generalAuthResp.myUserId
                });
            }
            else 
            {
                return BadRequest("Please provide all of the parameters");
            }
            
        }

        private string SqlFunGet(string cmd)
        {
            SqlConnection myConnection = new SqlConnection("Data Source=tcp:anastasia-seniorprojectdbserver.database.windows.net,1433;Initial Catalog=Anastasia_SeniorProject_db;User Id=tasoakhv@anastasia-seniorprojectdbserver;Password=Test12345678.");
            SqlDataAdapter myAdapter;
            DataTable myResult = new DataTable();
            myConnection.Open();
            string varCmd = cmd;
            myAdapter = new SqlDataAdapter(varCmd, myConnection);
            myAdapter.Fill(myResult);
            string resultingStr = null;

            foreach (DataRow dr in myResult.Rows)
            {

                for (int i = 0; i < dr.ItemArray.Length; i++)
                {
                    resultingStr = resultingStr + dr.ItemArray[i].ToString() + "\n";

                }
            }

            myConnection.Close();
            return resultingStr;
        }
    }
}
