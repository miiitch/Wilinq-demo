using System;
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
							 select workitem;

			result = allWIQuery.ToList();

			var projectWiQuery = from workitem in scrumProject.WorkItemSet()
								 select workitem;

			result = projectWiQuery.ToList();

		}
	}
}
