using PERMWebSolution.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PERMWebSolution.Controllers
{
    public class PhaseActivityController : ApiController
    {
        [HttpGet]
        [Route("api/PhaseActivity/GetPhaseActivity/")] //if you pass string by GET method from Ajax, MAKE SURE, that Ajax URI ends with leading "/" symbol ->>> it is important, otherwise this route method will not be resolved!
        public IHttpActionResult GetPhaseActivity()
        {
            //var a = table;
            IHttpActionResult ret = null;

            List<PhaseActivity> phaseActivityList = PhaseActivity.getPhaseActivity();
            if (phaseActivityList.Any())
            {
                ret = Ok(phaseActivityList);
            }
            

      
            return ret;
        }

    }
}
