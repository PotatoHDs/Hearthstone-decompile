using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using Blizzard.T5.Services;
using UnityEngine;

namespace Hearthstone
{
	// Token: 0x02000FD8 RID: 4056
	public static class HearthstoneJobs
	{
		// Token: 0x0600B086 RID: 45190 RVA: 0x00368839 File Offset: 0x00366A39
		public static JobDefinition CreateJobFromDependency(string jobID, IJobDependency dependency)
		{
			return HearthstoneJobs.CreateJobFromDependency(jobID, dependency, JobFlags.None);
		}

		// Token: 0x0600B087 RID: 45191 RVA: 0x00368844 File Offset: 0x00366A44
		public static JobDefinition CreateJobFromDependency(string jobID, IJobDependency dependency, JobFlags jobFlags)
		{
			IJobDependency[] dependencies = null;
			return new JobDefinition(jobID, HearthstoneJobs.Job_YieldDependency(dependency), jobFlags, dependencies);
		}

		// Token: 0x0600B088 RID: 45192 RVA: 0x00368861 File Offset: 0x00366A61
		public static JobDefinition CreateJobFromDependency(string jobID, IJobDependency dependency, params IJobDependency[] dependencies)
		{
			return HearthstoneJobs.CreateJobFromDependency(jobID, dependency, JobFlags.None, dependencies);
		}

		// Token: 0x0600B089 RID: 45193 RVA: 0x0036886C File Offset: 0x00366A6C
		public static JobDefinition CreateJobFromDependency(string jobID, IJobDependency dependency, JobFlags jobFlags, params IJobDependency[] dependencies)
		{
			return new JobDefinition(jobID, HearthstoneJobs.Job_YieldDependency(dependency), jobFlags, dependencies);
		}

		// Token: 0x0600B08A RID: 45194 RVA: 0x0036887C File Offset: 0x00366A7C
		public static JobDefinition CreateJobFromDependency(string jobID, IJobDependency dependency, params object[] dependenciesIdentifiers)
		{
			return HearthstoneJobs.CreateJobFromDependency(jobID, dependency, JobFlags.None, dependenciesIdentifiers);
		}

		// Token: 0x0600B08B RID: 45195 RVA: 0x00368887 File Offset: 0x00366A87
		public static JobDefinition CreateJobFromDependency(string jobID, IJobDependency dependency, JobFlags jobFlags, params object[] dependenciesIdentifiers)
		{
			return new JobDefinition(jobID, HearthstoneJobs.Job_YieldDependency(dependency), jobFlags, HearthstoneJobs.BuildDependencies(dependenciesIdentifiers));
		}

		// Token: 0x0600B08C RID: 45196 RVA: 0x0036889C File Offset: 0x00366A9C
		public static JobDefinition CreateJobFromAction(string jobID, Action action)
		{
			return HearthstoneJobs.CreateJobFromAction(jobID, action, JobFlags.None);
		}

		// Token: 0x0600B08D RID: 45197 RVA: 0x003688A8 File Offset: 0x00366AA8
		public static JobDefinition CreateJobFromAction(string jobID, Action action, JobFlags jobFlags)
		{
			IJobDependency[] dependencies = null;
			return new JobDefinition(jobID, HearthstoneJobs.Job_InvokeAction(action), jobFlags, dependencies);
		}

		// Token: 0x0600B08E RID: 45198 RVA: 0x003688C5 File Offset: 0x00366AC5
		public static JobDefinition CreateJobFromAction(string jobID, Action action, params IJobDependency[] dependencies)
		{
			return HearthstoneJobs.CreateJobFromAction(jobID, action, JobFlags.None, dependencies);
		}

		// Token: 0x0600B08F RID: 45199 RVA: 0x003688D0 File Offset: 0x00366AD0
		public static JobDefinition CreateJobFromAction(string jobID, Action action, JobFlags jobFlags, params IJobDependency[] dependencies)
		{
			return new JobDefinition(jobID, HearthstoneJobs.Job_InvokeAction(action), jobFlags, dependencies);
		}

		// Token: 0x0600B090 RID: 45200 RVA: 0x003688E0 File Offset: 0x00366AE0
		public static JobDefinition CreateJobFromAction(string jobID, Action action, params object[] dependenciesIdentifiers)
		{
			return HearthstoneJobs.CreateJobFromAction(jobID, action, JobFlags.None, dependenciesIdentifiers);
		}

		// Token: 0x0600B091 RID: 45201 RVA: 0x003688EB File Offset: 0x00366AEB
		public static JobDefinition CreateJobFromAction(string jobID, Action action, JobFlags jobFlags, params object[] dependenciesIdentifiers)
		{
			return new JobDefinition(jobID, HearthstoneJobs.Job_InvokeAction(action), jobFlags, HearthstoneJobs.BuildDependencies(dependenciesIdentifiers));
		}

		// Token: 0x0600B092 RID: 45202 RVA: 0x00368900 File Offset: 0x00366B00
		public static IEnumerator<IAsyncJobResult> Job_YieldDependency(IJobDependency dependency)
		{
			yield return dependency;
			yield break;
		}

		// Token: 0x0600B093 RID: 45203 RVA: 0x0036890F File Offset: 0x00366B0F
		public static IEnumerator<IAsyncJobResult> Job_InvokeAction(Action action)
		{
			if (action != null)
			{
				if (action.Target is UnityEngine.Object && action.Target.Equals(null))
				{
					yield break;
				}
				action();
			}
			yield break;
		}

		// Token: 0x0600B094 RID: 45204 RVA: 0x00368920 File Offset: 0x00366B20
		public static IJobDependency[] BuildDependencies(params object[] dependencyIdentifiers)
		{
			HearthstoneJobs.s_dependencyBuilder.Clear();
			foreach (object obj in dependencyIdentifiers)
			{
				IJobDependency jobDependency = obj as IJobDependency;
				if (jobDependency != null)
				{
					HearthstoneJobs.s_dependencyBuilder.Add(jobDependency);
				}
				else
				{
					Type type = obj as Type;
					if (type != null && typeof(IService).IsAssignableFrom(type))
					{
						HearthstoneJobs.s_dependencyBuilder.Add(HearthstoneServices.CreateServiceDependency(type));
					}
					else
					{
						Log.Jobs.PrintWarning("[HearthstoneJobs.BuildDependencies] Failed to create job dependency from identifier ({0}).", new object[]
						{
							obj
						});
					}
				}
			}
			if (HearthstoneJobs.s_dependencyBuilder.Count <= 0)
			{
				return null;
			}
			return HearthstoneJobs.s_dependencyBuilder.ToArray();
		}

		// Token: 0x0400953C RID: 38204
		private static List<IJobDependency> s_dependencyBuilder = new List<IJobDependency>();
	}
}
