using System;
using System.Collections.Generic;
using UnityEngine;

namespace Blizzard.T5.Jobs
{
	// Token: 0x02001174 RID: 4468
	public class JobQueueTelemetry
	{
		// Token: 0x0600C416 RID: 50198 RVA: 0x003B2B87 File Offset: 0x003B0D87
		public JobQueueTelemetry(JobQueue jobQueue, JobQueueAlerts jobQueueAlerts, string testType)
		{
			this.m_jobQueue = jobQueue;
			this.m_testType = testType;
			this.m_jobQueue.AddJobErrorListener(new JobQueue.JobErrorListener(this.OnJobError));
		}

		// Token: 0x0600C417 RID: 50199 RVA: 0x003B2BC0 File Offset: 0x003B0DC0
		private bool OnJobStart(JobDefinition job)
		{
			float realtimeSinceStartup = Time.realtimeSinceStartup;
			this.m_jobDurations[job] = realtimeSinceStartup;
			TelemetryManager.Client().SendJobBegin(job.Name, this.m_testType);
			return false;
		}

		// Token: 0x0600C418 RID: 50200 RVA: 0x003B2BF8 File Offset: 0x003B0DF8
		public bool OnJobSuccess(JobDefinition job)
		{
			float duration = this.CalculateJobDuration(job, Time.realtimeSinceStartup);
			TelemetryManager.Client().SendJobFinishSuccess(job.Name, this.m_testType, 2134675.ToString(), duration);
			return false;
		}

		// Token: 0x0600C419 RID: 50201 RVA: 0x003B2C38 File Offset: 0x003B0E38
		private bool OnJobError(JobDefinition job, string reason)
		{
			float duration = this.CalculateJobDuration(job, Time.realtimeSinceStartup);
			TelemetryManager.Client().SendJobFinishFailure(job.Name, reason, this.m_testType, 2134675.ToString(), duration);
			return false;
		}

		// Token: 0x0600C41A RID: 50202 RVA: 0x003B2C78 File Offset: 0x003B0E78
		private void OnJobLengthExceeded(JobDefinition job, long duration)
		{
			TelemetryManager.Client().SendJobExceededLimit(job.Name, duration, this.m_testType);
		}

		// Token: 0x0600C41B RID: 50203 RVA: 0x003B2C91 File Offset: 0x003B0E91
		private void OnJobStepExcceeded(JobDefinition job, long duration)
		{
			TelemetryManager.Client().SendJobStepExceededLimit(job.Name, duration, this.m_testType);
		}

		// Token: 0x0600C41C RID: 50204 RVA: 0x000D52B1 File Offset: 0x000D34B1
		private float CalculateJobDuration(JobDefinition job, float jobEndTime)
		{
			return 0f;
		}

		// Token: 0x04009D0E RID: 40206
		private JobQueue m_jobQueue;

		// Token: 0x04009D0F RID: 40207
		private string m_testType;

		// Token: 0x04009D10 RID: 40208
		private Dictionary<JobDefinition, float> m_jobDurations = new Dictionary<JobDefinition, float>();
	}
}
