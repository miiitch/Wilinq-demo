﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.WorkItemTracking.Client;
using WiLinq.LinqProvider;
namespace QueryDemoConsole
{
	class Program
	{
		static void Main(string[] args)
		{
			string collectionUrl = "http://localhost:8080/tfs/DefaultCollection";
			var teamProject = "Scrum";


			List<WorkItem> result;

			TfsTeamProjectCollection tpc = new TfsTeamProjectCollection(new Uri(collectionUrl));

			tpc.Authenticate();


			WorkItemStore store = tpc.GetService<WorkItemStore>();

			var scrumProject = store.Projects.Cast<Project>().First(_ => _.Name == teamProject);

			//all workitems;
			var allWIQuery = from workitem in tpc.WorkItemSet()
							 where workitem.Id == 2
							 select workitem;

			result = allWIQuery.ToList();
           

			var projectWiQuery = from workitem in scrumProject.WorkItemSet()								
								 where workitem.Title.Contains("Build")								 
								 && workitem.Field<string>(SystemField.AssignedTo) == QueryConstant.Me
								 select workitem;

			result = projectWiQuery.ToList();


            // Check if the mapping is supported
            if (scrumProject.IsSupported<Bug>())
            {
                // this query only works for Scrum template 2.0 
                var bugQuery = from bug in scrumProject.SetOf<Bug>()
                               where bug.Title == "Build Failure in Build: MonApp_20130328.2" && bug.AssignedTo == QueryConstant.Me
                               select bug;

                var bugResult = bugQuery.ToList();
            }

		}
	}
}
