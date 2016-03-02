using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PERMWebSolution.Models
{
    public class PhaseActivity
    {
        public int activityId { get; set; }
        public string Name { get; set; }
        public  string isActivated { get; set; }




        public static List<PhaseActivity> getPhaseActivity()
        {
            List<PhaseActivity> activities = new List<PhaseActivity>();
            Random rand = new Random();
            for(int i = 0; i <=6; i++)
            {
                activities.Add(new PhaseActivity { activityId = i, Name = (rand.Next(1, 10) > 5) ? "Cheeseburgers" : "", isActivated = (rand.Next(0, 10)>5 ? "checked" : "") });

            }


            return activities;

        }
    }

   
}