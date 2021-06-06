using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Blizzard.T5.Core;
using Blizzard.T5.Jobs;
using UnityEngine;

namespace Hearthstone.Core
{
	// Token: 0x0200107D RID: 4221
	public class Processor : MonoBehaviour
	{
		// Token: 0x17000A25 RID: 2597
		// (get) Token: 0x0600B657 RID: 46679 RVA: 0x0037EEB0 File Offset: 0x0037D0B0
		private static Processor Instance
		{
			get
			{
				if (Processor.s_instance == null)
				{
					Processor.s_instance = UnityEngine.Object.FindObjectOfType<Processor>();
					if (Processor.s_instance == null)
					{
						GameObject gameObject = new GameObject("Processor");
						gameObject.hideFlags |= HideFlags.HideAndDontSave;
						Processor.s_instance = gameObject.AddComponent<Processor>();
					}
					LinkedList<Processor.SchedulerContext> scheduledCallbacks = Processor.s_instance.m_scheduledCallbacks;
					lock (scheduledCallbacks)
					{
						Processor.s_instance.m_lastRealtimeSinceStartup = Time.realtimeSinceStartup;
					}
					if (Application.isPlaying)
					{
						UnityEngine.Object.DontDestroyOnLoad(Processor.s_instance.gameObject);
					}
					Processor.s_instance.EnsureJobQueueExists();
				}
				return Processor.s_instance;
			}
		}

		// Token: 0x17000A26 RID: 2598
		// (get) Token: 0x0600B658 RID: 46680 RVA: 0x0037EF6C File Offset: 0x0037D16C
		public static bool IsReady
		{
			get
			{
				return Processor.s_instance != null;
			}
		}

		// Token: 0x17000A27 RID: 2599
		// (get) Token: 0x0600B659 RID: 46681 RVA: 0x0037EF79 File Offset: 0x0037D179
		public static JobQueue JobQueue
		{
			get
			{
				return Processor.Instance.m_jobQueue;
			}
		}

		// Token: 0x17000A28 RID: 2600
		// (get) Token: 0x0600B65A RID: 46682 RVA: 0x0037EF85 File Offset: 0x0037D185
		// (set) Token: 0x0600B65B RID: 46683 RVA: 0x0037EF94 File Offset: 0x0037D194
		public static bool UseJobQueueAlerts
		{
			get
			{
				return Processor.Instance.m_jobQueueAlerts != null;
			}
			set
			{
				if (Processor.Instance.m_jobQueueAlerts == null && value)
				{
					Processor.Instance.m_jobQueueAlerts = new JobQueueAlerts(Processor.Instance.m_jobQueue, Log.Jobs);
					return;
				}
				if (Processor.Instance.m_jobQueueAlerts != null && !value)
				{
					Processor.Instance.m_jobQueueAlerts = null;
				}
			}
		}

		// Token: 0x17000A29 RID: 2601
		// (get) Token: 0x0600B65C RID: 46684 RVA: 0x0037EFEB File Offset: 0x0037D1EB
		public static JobQueueAlerts JobQueueAlerts
		{
			get
			{
				return Processor.Instance.m_jobQueueAlerts;
			}
		}

		// Token: 0x17000A2A RID: 2602
		// (get) Token: 0x0600B65D RID: 46685 RVA: 0x0037EFF7 File Offset: 0x0037D1F7
		public static string JobQueueDirectoryPath
		{
			get
			{
				return Processor.s_jqdDirectoryOverride ?? Path.Combine(Application.persistentDataPath, "Logs", "JobQueueData");
			}
		}

		// Token: 0x0600B65E RID: 46686 RVA: 0x0037F016 File Offset: 0x0037D216
		private void Awake()
		{
			if (Processor.s_instance == null)
			{
				Processor.s_instance = this;
				this.EnsureJobQueueExists();
			}
		}

		// Token: 0x0600B65F RID: 46687 RVA: 0x0037F031 File Offset: 0x0037D231
		private void Update()
		{
			this.ProcessUpdateDelegates();
			this.ProcessJobQueue();
			this.ProcessScheduledCallbacks();
			if (this.m_isJobQueueMonitorEnabled)
			{
				JobQueueMonitor.Update();
			}
		}

		// Token: 0x0600B660 RID: 46688 RVA: 0x0037F052 File Offset: 0x0037D252
		private void LateUpdate()
		{
			this.ProcessLateUpdateDelegates();
		}

		// Token: 0x0600B661 RID: 46689 RVA: 0x0037F05A File Offset: 0x0037D25A
		private void OnApplicationPause(bool pauseState)
		{
			if (pauseState && this.m_isJobQueueMonitorEnabled)
			{
				Processor.OutputJobQueue();
			}
		}

		// Token: 0x0600B662 RID: 46690 RVA: 0x0037F06C File Offset: 0x0037D26C
		public static void SetTrackedQueueDataFilePrefix(string newPrefix)
		{
			Processor.s_jqdPrefix = newPrefix;
		}

		// Token: 0x0600B663 RID: 46691 RVA: 0x0037F074 File Offset: 0x0037D274
		public static void SetTrackedQueueDataDirectory(string directory)
		{
			Processor.s_jqdDirectoryOverride = directory;
		}

		// Token: 0x0600B664 RID: 46692 RVA: 0x0037F07C File Offset: 0x0037D27C
		public static void OutputJobQueue()
		{
			string jobQueueDirectoryPath = Processor.JobQueueDirectoryPath;
			for (int i = 0; i < JobQueueMonitor.TrackedJobQueues.Count; i++)
			{
				string text = Path.Combine(jobQueueDirectoryPath, string.Format("{0}_{1}.jqd", Processor.s_jqdPrefix, i + 1));
				JobQueueMonitor.TrackedJobQueue trackedJobQueue = JobQueueMonitor.TrackedJobQueues[i];
				trackedJobQueue.CaptureTime();
				string contents = JsonUtility.ToJson(trackedJobQueue);
				try
				{
					if (Directory.Exists(jobQueueDirectoryPath))
					{
						if (File.Exists(text))
						{
							File.SetAttributes(text, FileAttributes.Normal);
							File.Delete(text);
						}
					}
					else
					{
						Directory.CreateDirectory(jobQueueDirectoryPath);
					}
					File.WriteAllText(text, contents);
				}
				catch
				{
					Log.Jobs.PrintError("Failed to write job queue data to filepath " + text, Array.Empty<object>());
				}
			}
		}

		// Token: 0x0600B665 RID: 46693 RVA: 0x0037F140 File Offset: 0x0037D340
		public static void RegisterUpdateDelegate(Action updateDelegate)
		{
			if (updateDelegate == null)
			{
				return;
			}
			if (!Processor.Instance.m_updateDelegates.Contains(updateDelegate))
			{
				Processor.Instance.m_updateDelegates.Add(updateDelegate);
			}
		}

		// Token: 0x0600B666 RID: 46694 RVA: 0x0037F168 File Offset: 0x0037D368
		public static void UnregisterUpdateDelegate(Action updateDelegate)
		{
			if (updateDelegate == null)
			{
				return;
			}
			Processor.Instance.m_updateDelegates.Remove(updateDelegate);
		}

		// Token: 0x0600B667 RID: 46695 RVA: 0x0037F17F File Offset: 0x0037D37F
		public static void RegisterLateUpdateDelegate(Action updateDelegate)
		{
			if (updateDelegate == null)
			{
				return;
			}
			if (!Processor.Instance.m_lateUpdateDelegates.Contains(updateDelegate))
			{
				Processor.Instance.m_lateUpdateDelegates.Add(updateDelegate);
			}
		}

		// Token: 0x0600B668 RID: 46696 RVA: 0x0037F1A7 File Offset: 0x0037D3A7
		public static void UnregisterLateUpdateDelegate(Action updateDelegate)
		{
			if (updateDelegate == null)
			{
				return;
			}
			Processor.Instance.m_lateUpdateDelegates.Remove(updateDelegate);
		}

		// Token: 0x0600B669 RID: 46697 RVA: 0x0037F1C0 File Offset: 0x0037D3C0
		public static void RegisterOnGUIDelegate(Action GUIDelegate)
		{
			if (GUIDelegate == null)
			{
				return;
			}
			if (!Processor.Instance.m_hasGUIDelegatComponent)
			{
				Processor.Instance.m_GUIDelegateComponent = Processor.Instance.gameObject.AddComponent<OnGUIDelegateComponent>();
				Processor.Instance.m_hasGUIDelegatComponent = true;
			}
			Processor.Instance.m_GUIDelegateComponent.AddOnGUIDelegate(GUIDelegate);
		}

		// Token: 0x0600B66A RID: 46698 RVA: 0x0037F211 File Offset: 0x0037D411
		public static void UnregisterOnGUIDelegate(Action GUIDelegate)
		{
			if (GUIDelegate == null || !Processor.Instance.m_hasGUIDelegatComponent)
			{
				return;
			}
			Processor.Instance.m_GUIDelegateComponent.RemoveOnGUIDelegate(GUIDelegate);
		}

		// Token: 0x0600B66B RID: 46699 RVA: 0x0037F233 File Offset: 0x0037D433
		public static bool ScheduleCallback(float secondsToWait, bool realTime, Processor.ScheduledCallback cb, object userData = null)
		{
			return Processor.s_instance.ScheduleCallbackInternal(secondsToWait, realTime, cb, userData);
		}

		// Token: 0x0600B66C RID: 46700 RVA: 0x0037F243 File Offset: 0x0037D443
		public static bool CancelScheduledCallback(Processor.ScheduledCallback cb, object userData = null)
		{
			return Processor.s_instance.CancelScheduledCallbackInternal(cb, userData);
		}

		// Token: 0x0600B66D RID: 46701 RVA: 0x0037F254 File Offset: 0x0037D454
		public static Coroutine RunCoroutine(IEnumerator routine, object objectRef = null)
		{
			if (Processor.s_instance.m_coroutines == null)
			{
				Processor.s_instance.m_coroutines = new Map<object, List<IEnumerator>>();
			}
			List<IEnumerator> list = null;
			if (objectRef != null)
			{
				if (Processor.s_instance.m_coroutines.TryGetValue(objectRef, out list))
				{
					list.Add(routine);
				}
				else
				{
					List<IEnumerator> list2 = new List<IEnumerator>();
					list2.Add(routine);
					Processor.s_instance.m_coroutines.Add(objectRef, list2);
				}
			}
			return Processor.s_instance.StartCoroutine(routine);
		}

		// Token: 0x0600B66E RID: 46702 RVA: 0x0037F2C7 File Offset: 0x0037D4C7
		public static void CancelCoroutine(IEnumerator routine)
		{
			Processor.s_instance.StopCoroutine(routine);
		}

		// Token: 0x0600B66F RID: 46703 RVA: 0x0037F2D4 File Offset: 0x0037D4D4
		public static void CancelCoroutine(Coroutine routine)
		{
			Processor.s_instance.StopCoroutine(routine);
		}

		// Token: 0x0600B670 RID: 46704 RVA: 0x0037F2E4 File Offset: 0x0037D4E4
		public static void StopAllCoroutinesWithObjectRef(object objectRef)
		{
			List<IEnumerator> list = null;
			if (Processor.s_instance.m_coroutines.TryGetValue(objectRef, out list))
			{
				foreach (IEnumerator routine in list)
				{
					Processor.s_instance.StopCoroutine(routine);
				}
				Processor.s_instance.m_coroutines.Remove(objectRef);
			}
		}

		// Token: 0x0600B671 RID: 46705 RVA: 0x0037F360 File Offset: 0x0037D560
		public static void StopCoroutines()
		{
			Processor.s_instance.StopAllCoroutines();
			Processor.s_instance.m_coroutines.Clear();
		}

		// Token: 0x0600B672 RID: 46706 RVA: 0x0037F37B File Offset: 0x0037D57B
		public static bool QueueJob(JobDefinition job)
		{
			return Processor.JobQueue.QueueJob(job);
		}

		// Token: 0x0600B673 RID: 46707 RVA: 0x0037F388 File Offset: 0x0037D588
		public static bool QueueJobIfNotExist(JobDefinition job)
		{
			return Processor.JobQueue.QueueJobIfNotExist(job);
		}

		// Token: 0x0600B674 RID: 46708 RVA: 0x0037F395 File Offset: 0x0037D595
		public static JobDefinition QueueJob(string id, IEnumerator<IAsyncJobResult> jobAction, params IJobDependency[] dependencies)
		{
			return Processor.JobQueue.QueueJob(id, jobAction, dependencies);
		}

		// Token: 0x0600B675 RID: 46709 RVA: 0x0037F3A4 File Offset: 0x0037D5A4
		public static JobDefinition QueueJobIfNotExist(string id, IEnumerator<IAsyncJobResult> jobAction, params IJobDependency[] dependencies)
		{
			return Processor.JobQueue.QueueJobIfNotExist(id, jobAction, dependencies);
		}

		// Token: 0x0600B676 RID: 46710 RVA: 0x0037F3B3 File Offset: 0x0037D5B3
		public static JobDefinition QueueJob(string id, IEnumerator<IAsyncJobResult> jobAction, JobFlags jobFlags, params IJobDependency[] dependencies)
		{
			return Processor.JobQueue.QueueJob(id, jobAction, jobFlags, dependencies);
		}

		// Token: 0x0600B677 RID: 46711 RVA: 0x0037F3C3 File Offset: 0x0037D5C3
		public static JobDefinition QueueJobIfNotExist(string id, IEnumerator<IAsyncJobResult> jobAction, JobFlags jobFlags, params IJobDependency[] dependencies)
		{
			return Processor.JobQueue.QueueJobIfNotExist(id, jobAction, jobFlags, dependencies);
		}

		// Token: 0x0600B678 RID: 46712 RVA: 0x0037F3D3 File Offset: 0x0037D5D3
		public static void TerminateAllProcessing()
		{
			Processor.s_instance.TerminateAllProcessingInternal();
		}

		// Token: 0x0600B679 RID: 46713 RVA: 0x0037F3DF File Offset: 0x0037D5DF
		public static bool IsJobQueueMonitorEnabled()
		{
			return PlayerPrefs.GetInt("JOB_QUEUE_MONITOR", 0) != 0;
		}

		// Token: 0x0600B67A RID: 46714 RVA: 0x0037F3EF File Offset: 0x0037D5EF
		public static void SetJobQueueMonitorEnabled(bool isEnabled)
		{
			PlayerPrefs.SetInt("JOB_QUEUE_MONITOR", isEnabled ? 1 : 0);
		}

		// Token: 0x0600B67B RID: 46715 RVA: 0x0037F404 File Offset: 0x0037D604
		private void EnsureJobQueueExists()
		{
			if (this.m_jobQueue == null)
			{
				this.m_jobQueue = new JobQueue(this.m_jobQueueFrameLimit);
				this.m_jobQueue.SetLogger(Log.Jobs);
				if (Processor.IsJobQueueMonitorEnabled())
				{
					JobQueueMonitor.StartMonitor(Log.Jobs, 600000f);
					this.m_isJobQueueMonitorEnabled = true;
				}
				this.m_jobQueueIterator = this.m_jobQueue.Process();
			}
		}

		// Token: 0x0600B67C RID: 46716 RVA: 0x0037F46C File Offset: 0x0037D66C
		private void ProcessUpdateDelegates()
		{
			for (int i = 0; i < this.m_updateDelegates.Count; i++)
			{
				if (this.m_updateDelegates[i] == null)
				{
					this.m_updateDelegates.RemoveAt(i);
					i--;
				}
				else
				{
					this.m_updateDelegates[i]();
				}
			}
		}

		// Token: 0x0600B67D RID: 46717 RVA: 0x0037F4C0 File Offset: 0x0037D6C0
		private void ProcessLateUpdateDelegates()
		{
			for (int i = 0; i < this.m_lateUpdateDelegates.Count; i++)
			{
				if (this.m_lateUpdateDelegates[i] == null)
				{
					this.m_lateUpdateDelegates.RemoveAt(i);
					i--;
				}
				else
				{
					this.m_lateUpdateDelegates[i]();
				}
			}
		}

		// Token: 0x0600B67E RID: 46718 RVA: 0x0037F514 File Offset: 0x0037D714
		private void ProcessJobQueue()
		{
			this.EnsureJobQueueExists();
			if (this.m_jobQueue != null && (this.m_jobQueueIterator == null || !this.m_jobQueueIterator.MoveNext()))
			{
				this.m_jobQueueIterator = this.m_jobQueue.Process();
			}
		}

		// Token: 0x0600B67F RID: 46719 RVA: 0x0037F54C File Offset: 0x0037D74C
		private void ProcessScheduledCallbacks()
		{
			LinkedList<Processor.SchedulerContext> scheduledCallbacks = this.m_scheduledCallbacks;
			lock (scheduledCallbacks)
			{
				float realtimeSinceStartup = Time.realtimeSinceStartup;
				this.m_lastRealtimeSinceStartup = realtimeSinceStartup;
				if (this.m_scheduledCallbacks.Count == 0)
				{
					return;
				}
				LinkedListNode<Processor.SchedulerContext> linkedListNode = this.m_scheduledCallbacks.First;
				while (linkedListNode != null)
				{
					Processor.SchedulerContext value = linkedListNode.Value;
					if (value.m_realTime)
					{
						value.m_secondsWaited = realtimeSinceStartup - value.m_startTime;
					}
					else
					{
						value.m_secondsWaited += Time.deltaTime;
					}
					if (value.m_secondsWaited >= value.m_secondsToWait)
					{
						this.m_readyCallbacks.AddLast(value);
						LinkedListNode<Processor.SchedulerContext> next = linkedListNode.Next;
						this.m_scheduledCallbacks.Remove(linkedListNode);
						linkedListNode = next;
					}
					else if (!GeneralUtils.IsCallbackValid(value.m_callback))
					{
						LinkedListNode<Processor.SchedulerContext> next2 = linkedListNode.Next;
						this.m_scheduledCallbacks.Remove(linkedListNode);
						linkedListNode = next2;
					}
					else
					{
						linkedListNode = linkedListNode.Next;
					}
				}
			}
			foreach (Processor.SchedulerContext schedulerContext in this.m_readyCallbacks)
			{
				if (GeneralUtils.IsCallbackValid(schedulerContext.m_callback))
				{
					schedulerContext.m_callback(schedulerContext.m_userData);
				}
			}
			this.m_readyCallbacks.Clear();
		}

		// Token: 0x0600B680 RID: 46720 RVA: 0x0037F6B8 File Offset: 0x0037D8B8
		private bool ScheduleCallbackInternal(float secondsToWait, bool realTime, Processor.ScheduledCallback cb, object userData = null)
		{
			LinkedList<Processor.SchedulerContext> scheduledCallbacks = this.m_scheduledCallbacks;
			lock (scheduledCallbacks)
			{
				foreach (Processor.SchedulerContext schedulerContext in this.m_scheduledCallbacks)
				{
					if (!(schedulerContext.m_callback != cb) && schedulerContext.m_userData == userData)
					{
						return false;
					}
				}
				Processor.SchedulerContext schedulerContext2 = new Processor.SchedulerContext();
				schedulerContext2.m_startTime = this.m_lastRealtimeSinceStartup;
				schedulerContext2.m_secondsToWait = secondsToWait;
				schedulerContext2.m_realTime = realTime;
				schedulerContext2.m_callback = cb;
				schedulerContext2.m_userData = userData;
				float num = schedulerContext2.EstimateTargetTime();
				bool flag2 = false;
				for (LinkedListNode<Processor.SchedulerContext> linkedListNode = this.m_scheduledCallbacks.Last; linkedListNode != null; linkedListNode = linkedListNode.Previous)
				{
					if (linkedListNode.Value.EstimateTargetTime() <= num)
					{
						flag2 = true;
						this.m_scheduledCallbacks.AddAfter(linkedListNode, schedulerContext2);
						break;
					}
				}
				if (!flag2)
				{
					this.m_scheduledCallbacks.AddFirst(schedulerContext2);
				}
			}
			return true;
		}

		// Token: 0x0600B681 RID: 46721 RVA: 0x0037F7E0 File Offset: 0x0037D9E0
		private bool CancelScheduledCallbackInternal(Processor.ScheduledCallback cb, object userData = null)
		{
			LinkedList<Processor.SchedulerContext> scheduledCallbacks = this.m_scheduledCallbacks;
			bool result;
			lock (scheduledCallbacks)
			{
				if (this.m_scheduledCallbacks.Count == 0)
				{
					result = false;
				}
				else
				{
					for (LinkedListNode<Processor.SchedulerContext> linkedListNode = this.m_scheduledCallbacks.First; linkedListNode != null; linkedListNode = linkedListNode.Next)
					{
						Processor.SchedulerContext value = linkedListNode.Value;
						if (value.m_callback == cb && value.m_userData == userData)
						{
							this.m_scheduledCallbacks.Remove(linkedListNode);
							return true;
						}
					}
					result = false;
				}
			}
			return result;
		}

		// Token: 0x0600B682 RID: 46722 RVA: 0x0037F878 File Offset: 0x0037DA78
		private void TerminateAllProcessingInternal()
		{
			this.m_updateDelegates.Clear();
			UnityEngine.Object.Destroy(this.m_GUIDelegateComponent);
			if (this.m_jobQueue != null)
			{
				this.m_jobQueue.ClearJobs();
				this.m_jobQueueIterator = this.m_jobQueue.Process();
			}
			LinkedList<Processor.SchedulerContext> scheduledCallbacks = this.m_scheduledCallbacks;
			lock (scheduledCallbacks)
			{
				this.m_scheduledCallbacks.Clear();
			}
			base.StopAllCoroutines();
			if (this.m_coroutines != null)
			{
				this.m_coroutines.Clear();
			}
		}

		// Token: 0x040097A7 RID: 38823
		[SerializeField]
		private float m_jobQueueFrameLimit;

		// Token: 0x040097A8 RID: 38824
		private List<Action> m_updateDelegates = new List<Action>();

		// Token: 0x040097A9 RID: 38825
		private List<Action> m_lateUpdateDelegates = new List<Action>();

		// Token: 0x040097AA RID: 38826
		private OnGUIDelegateComponent m_GUIDelegateComponent;

		// Token: 0x040097AB RID: 38827
		private bool m_hasGUIDelegatComponent;

		// Token: 0x040097AC RID: 38828
		private JobQueue m_jobQueue;

		// Token: 0x040097AD RID: 38829
		private IEnumerator m_jobQueueIterator;

		// Token: 0x040097AE RID: 38830
		private readonly LinkedList<Processor.SchedulerContext> m_scheduledCallbacks = new LinkedList<Processor.SchedulerContext>();

		// Token: 0x040097AF RID: 38831
		private readonly LinkedList<Processor.SchedulerContext> m_readyCallbacks = new LinkedList<Processor.SchedulerContext>();

		// Token: 0x040097B0 RID: 38832
		private JobQueueAlerts m_jobQueueAlerts;

		// Token: 0x040097B1 RID: 38833
		private Map<object, List<IEnumerator>> m_coroutines;

		// Token: 0x040097B2 RID: 38834
		private static Processor s_instance = null;

		// Token: 0x040097B3 RID: 38835
		private float m_lastRealtimeSinceStartup;

		// Token: 0x040097B4 RID: 38836
		private bool m_isJobQueueMonitorEnabled;

		// Token: 0x040097B5 RID: 38837
		private static string s_jqdPrefix = "job_queue_data";

		// Token: 0x040097B6 RID: 38838
		private static string s_jqdDirectoryOverride = null;

		// Token: 0x02002885 RID: 10373
		private class SchedulerContext
		{
			// Token: 0x06013C27 RID: 80935 RVA: 0x0053C169 File Offset: 0x0053A369
			public float EstimateTargetTime()
			{
				return this.m_startTime + (this.m_realTime ? this.m_secondsToWait : (this.m_secondsToWait * TimeScaleMgr.Get().GetGameTimeScale()));
			}

			// Token: 0x0400F9D9 RID: 63961
			public float m_startTime;

			// Token: 0x0400F9DA RID: 63962
			public float m_secondsToWait;

			// Token: 0x0400F9DB RID: 63963
			public bool m_realTime;

			// Token: 0x0400F9DC RID: 63964
			public Processor.ScheduledCallback m_callback;

			// Token: 0x0400F9DD RID: 63965
			public object m_userData;

			// Token: 0x0400F9DE RID: 63966
			public float m_secondsWaited;
		}

		// Token: 0x02002886 RID: 10374
		// (Invoke) Token: 0x06013C2A RID: 80938
		public delegate void ScheduledCallback(object userData);
	}
}
