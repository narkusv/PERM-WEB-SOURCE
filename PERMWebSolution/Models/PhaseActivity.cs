using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PERMWebSolution.Models
{
    public class PhaseActivity
    {
     
        public string Name { get; set; }
   



        public static List<PhaseActivity> getPhaseActivity()
        {
            List<PhaseActivity> activities = new List<PhaseActivity>();
            Random rand = new Random();
            for(int i = 0; i <=6; i++)
            {
                activities.Add(new PhaseActivity { Name = (rand.Next(1, 10) > 5) ? "Cheeseburgers" : ""});

            }


            return activities;

        }


        /// <summary>
        /// activity add method
        /// </summary>
        /// <param name="activityList">List of activity names</param>
        /// <returns>string result of activity data converted back to activity from frontend</returns>
        public bool savePhaseActivity(List<PhaseActivity> activityList)
        {
            return true;
        }
    }

   
}