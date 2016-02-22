using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PERMWebSolution.Models;

namespace PERMWebSolution.Controllers
{
    /*Although it’s not necessary, I like adding the attribute [HttpGet()] in front of the method to be very explicit about which HTTP verb this method supports. 
        Declare a variable named ret, of the type IHttpActionResult. Declare a variable named list, to hold a collection of product objects. 
        Build the list of data by calling the CreateMockData method that you defined earlier. 
        Set the ret variable to the Ok method built into the ApiController class, passing in the list of product objects. 
        The Ok method does a couple of things; it sets the HTTP status code to 200, 
        and it includes the list of products in the HttpResponseMessage sent back from this API call.*/
    public class UserController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/UserControler
        [HttpPost]
        public IHttpActionResult Post(User usr)
        {

            IHttpActionResult ret = null;
            //var session = usr.logInUser(usr);

            if (usr.saveUser(usr))
            {
                ret = Ok();
            }
            else
            {
                ret = ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.Forbidden, new HttpError("User cannot be saved! Try again")));
            }
            return ret;

         /*   if (usr.logInUser(usr))
            {
                ret = Ok(usr.sessionID);
            }
            else
            {
                // http://stackoverflow.com/questions/23820312/httperror-and-ihttpactionresult
                
                ret =  ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.Forbidden, new HttpError("User name is already registered")));
            }
            return ret;*/
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}