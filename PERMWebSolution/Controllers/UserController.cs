using System.Net;
using System.Net.Http;
using System.Web.Http;
using PERMWebSolution.Models;

namespace PERMWebSolution.Controllers
{
    /* MUST READ!
    Although it’s not necessary, I like adding the attribute [HttpGet()] in front of the method to be very explicit about which HTTP verb this method supports. 
    Declare a variable named ret, of the type IHttpActionResult.  
    Set the ret variable to the Ok method built into the ApiController class. If you need parameters to return pass them back inside Ok() method e.g. Ok(param). 
    The Ok method does a couple of things; it sets the HTTP status code to 200, 
    and it includes the content response in the HttpResponseMessage that must be sent back from API call.
        */


    public class UserController : ApiController
    {
        private User usr;
        /// <summary>
        /// Sample no parameter GET method, irf not used should be deleted
        /// </summary>
        /// <returns>HTTP OK response</returns>
        [HttpGet] //explicitly state that this method can be only called by Ajax GET call
        [Route("api/Users/listUsers")] // path api/Users/ is standard at your controler it can be called whatever you like, the last is the name of the function that must be called
        public IHttpActionResult listUsers() //sample controler function call without any parameters by Ajax GET
        {
            IHttpActionResult ret = null;
            //do whatever you need to create output
            ret = Ok(); //set to HTTP code 200 -> browser understands it as Success
            return ret;
        }

        /// <summary>
        /// Sample one parameter GET method, if not used should be deleted
        /// </summary>
        /// <param name="id">param passed from Ajax in URI string</param>
        /// <returns></returns>
       [HttpGet]
       [Route("api/Users/listOneUser/{id}")] // {id} is the parameter you pass with URI request by GET method
        public IHttpActionResult listOneUser(int id)
        {
            IHttpActionResult ret = null;
            //do whatever needed to fetch back rsult back to browser
            ret = Ok("No");
            return ret;
        }

        //INFO hot to configure controler methods to accept [.] character in string//
        //http://haacked.com/archive/2010/04/29/allowing-reserved-filenames-in-URLs.aspx/ 
        //http://stackoverflow.com/questions/11720387/asp-net-mvc-does-not-receive-the-get-request-with-a-period-in-it/11736971#11736971
        //http://stackoverflow.com/questions/2831142/asp-net-4-url-limitations-why-url-cannot-contain-any-3f-characters 
        //need to configure Web.config, otherwise it will not work when passing period in string
        //finally this one solves problem http://stackoverflow.com/questions/20998816/dot-character-in-mvc-web-api-2-for-request-such-as-api-people-staff-45287
        /// <summary>
        /// Method checks if declared email adress in the registration form is valid -> i.e. not already used by other user
        /// </summary>
        /// <param name="email">Email passed as URI parameter</param>
        /// <returns>True if email is already occupied</returns>
        [HttpGet] //explicitly state that this method can be only called by Ajax GET call
        [Route("api/Users/checkEmail/{email}")] //if you pass string by GET method from Ajax, MAKE SURE, that Ajax URI ends with leading "/" symbol ->>> it is important, otherwise this route method will not be resolved!
        public IHttpActionResult checkEmail(string email)
        {
            IHttpActionResult ret = null;

            usr = new User();
            if (!usr.isUserNameExists(email))
            {

                ret = Ok();
            }
            else
            {
                ret = NotFound();
            }
            return ret;
        }
        /// <summary>
        /// Saves to save the user
        /// </summary>
        /// <param name="newUser">Type of User object passed in POST body</param>
        /// <returns>HTTP Ok 200 if user sucessfuly saved, othervise HttpStatusCode.Forbidden</returns>
        [HttpPost] //explicitly state that this method can be only called by Ajax POST call
        [Route("api/Users/saveUser/{newUser?}")] //{newUser?} is parameter included in POST body, the ? mark shows that parameter is NOT mandatory to this POST method
        public IHttpActionResult saveUser(User newUser) //
        {
            IHttpActionResult ret = null;
            if (newUser != null && newUser.saveUser(newUser))
            {
                ret = Ok(); //user saved this will call Ajax <success: > part
            }
            else
            {   // http://stackoverflow.com/questions/23820312/httperror-and-ihttpactionresult more on status codes
                ret = ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.Forbidden, "User cannot be saved! Try again")); //error has happened this will call Ajax <error: > if it is anything but the Ok() 
            }
            return ret;
        }
        /// <summary>
        /// Http PUT action The same scheme as above not used at the moment, thus delete if not used
        /// </summary>
        /// <param name="id">record of the id that has to be changed with the new 'value'</param>
        /// <param name="value">new substitute to the record that has passed to method 'id'</param>
        // PUT api/<controller>/5
        [HttpPut] //these will not be called because dos not have [Route] savybes
        public void Put(int id, [FromBody]string value) //[FormBody] check http://www.c-sharpcorner.com/UploadFile/dacca2/web-api-with-ajax-understand-formbody-and-formuri-attribute/
        {
        }

        /// <summary>
        ///  Http DELETE action The same scheme as above not used at the moment, thus delete if not used
        /// </summary>
        /// <param name="id">record of the id to be deleted</param>
        // DELETE api/<controller>/5
        [HttpDelete]//these will not be called because dos not have [Route] savybes
        public void Delete(int id)
        {
        }
    }
}