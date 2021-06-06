using System.Collections.Generic;
using UnityEngine;

namespace Blizzard.T5.Jobs
{
	public class JobQueueTelemetry
	{
		private JobQueue m_jobQueue;

		private string m_testType;

		private Dictionary<JobDefinition, float> m_jobDurations = new Dictionary<JobDefinition, float>();

		public JobQueueTelemetry(JobQueue jobQueue, JobQueueAlerts jobQueueAlerts, string testType)
		{
			m_jobQueue = jobQueue;
			m_testType = testType;
			m_jobQueue.AddJobErrorListener(OnJobError);
		}

		private bool OnJobStart(JobDefinition job)
		{
			float realtimeSinceStartup = Time.realtimeSinceStartup;
			m_jobDurations[job] = realtimeSinceStartup;
			TelemetryManager.Client().SendJobBegin(job.Name, m_testType);
			return false;
		}

		public bool OnJobSuccess(JobDefinition job)
		{
			float duration = CalculateJobDuration(job, Time.realtimeSinceStartup);
			TelemetryManager.Client().SendJobFinishSuccess(job.Name, m_testType, 2134675.ToString(), duration);
			return false;
		}

		private bool OnJobError(JobDefinition job, string reason)
		{
			float duration = CalculateJobDuration(job, Time.realtimeSinceStartup);
			TelemetryManager.Client().SendJobFinishFailure(job.Name, reason, m_testType, 2134675.ToString(), duration);
			return false;
		}

		private void OnJobLengthExceeded(JobDefinition job, long duration)
		{
			TelemetryManager.Client().SendJobExceededLimit(job.Name, duration, m_testType);
		}

		private void OnJobStepExcceeded(JobDefinition job, long duration)
		{
			TelemetryManager.Client().SendJobStepExceededLimit(job.Name, duration, m_testType);
		}

		private float CalculateJobDuration(JobDefinition job, float jobEndTime)
		{
			return 0f;
		}
	}
}
