using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Blizzard.T5.Core;
using Blizzard.T5.Jobs;
using UnityEngine;

namespace Hearthstone.Core
{
	public class Processor : MonoBehaviour
	{
		private class SchedulerContext
		{
			public float m_startTime;

			public float m_secondsToWait;

			public bool m_realTime;

			public ScheduledCallback m_callback;

			public object m_userData;

			public float m_secondsWaited;

			public float EstimateTargetTime()
			{
				return m_startTime + (m_realTime ? m_secondsToWait : (m_secondsToWait * TimeScaleMgr.Get().GetGameTimeScale()));
			}
		}

		public delegate void ScheduledCallback(object userData);

		[SerializeField]
		private float m_jobQueueFrameLimit;

		private List<Action> m_updateDelegates = new List<Action>();

		private List<Action> m_lateUpdateDelegates = new List<Action>();

		private OnGUIDelegateComponent m_GUIDelegateComponent;

		private bool m_hasGUIDelegatComponent;

		private JobQueue m_jobQueue;

		private IEnumerator m_jobQueueIterator;

		private readonly LinkedList<SchedulerContext> m_scheduledCallbacks = new LinkedList<SchedulerContext>();

		private readonly LinkedList<SchedulerContext> m_readyCallbacks = new LinkedList<SchedulerContext>();

		private JobQueueAlerts m_jobQueueAlerts;

		private Map<object, List<IEnumerator>> m_coroutines;

		private static Processor s_instance = null;

		private float m_lastRealtimeSinceStartup;

		private bool m_isJobQueueMonitorEnabled;

		private static string s_jqdPrefix = "job_queue_data";

		private static string s_jqdDirectoryOverride = null;

		private static Processor Instance
		{
			get
			{
				if (s_instance == null)
				{
					s_instance = UnityEngine.Object.FindObjectOfType<Processor>();
					if (s_instance == null)
					{
						GameObject obj = new GameObject("Processor");
						obj.hideFlags |= HideFlags.HideAndDontSave;
						s_instance = obj.AddComponent<Processor>();
					}
					lock (s_instance.m_scheduledCallbacks)
					{
						s_instance.m_lastRealtimeSinceStartup = Time.realtimeSinceStartup;
					}
					if (Application.isPlaying)
					{
						UnityEngine.Object.DontDestroyOnLoad(s_instance.gameObject);
					}
					s_instance.EnsureJobQueueExists();
				}
				return s_instance;
			}
		}

		public static bool IsReady => s_instance != null;

		public static JobQueue JobQueue => Instance.m_jobQueue;

		public static bool UseJobQueueAlerts
		{
			get
			{
				return Instance.m_jobQueueAlerts != null;
			}
			set
			{
				if (Instance.m_jobQueueAlerts == null && value)
				{
					Instance.m_jobQueueAlerts = new JobQueueAlerts(Instance.m_jobQueue, Log.Jobs);
				}
				else if (Instance.m_jobQueueAlerts != null && !value)
				{
					Instance.m_jobQueueAlerts = null;
				}
			}
		}

		public static JobQueueAlerts JobQueueAlerts => Instance.m_jobQueueAlerts;

		public static string JobQueueDirectoryPath => s_jqdDirectoryOverride ?? Path.Combine(Application.persistentDataPath, "Logs", "JobQueueData");

		private void Awake()
		{
			if (s_instance == null)
			{
				s_instance = this;
				EnsureJobQueueExists();
			}
		}

		private void Update()
		{
			ProcessUpdateDelegates();
			ProcessJobQueue();
			ProcessScheduledCallbacks();
			if (m_isJobQueueMonitorEnabled)
			{
				JobQueueMonitor.Update();
			}
		}

		private void LateUpdate()
		{
			ProcessLateUpdateDelegates();
		}

		private void OnApplicationPause(bool pauseState)
		{
			if (pauseState && m_isJobQueueMonitorEnabled)
			{
				OutputJobQueue();
			}
		}

		public static void SetTrackedQueueDataFilePrefix(string newPrefix)
		{
			s_jqdPrefix = newPrefix;
		}

		public static void SetTrackedQueueDataDirectory(string directory)
		{
			s_jqdDirectoryOverride = directory;
		}

		public static void OutputJobQueue()
		{
			string jobQueueDirectoryPath = JobQueueDirectoryPath;
			for (int i = 0; i < JobQueueMonitor.TrackedJobQueues.Count; i++)
			{
				string text = Path.Combine(jobQueueDirectoryPath, $"{s_jqdPrefix}_{i + 1}.jqd");
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
					Log.Jobs.PrintError("Failed to write job queue data to filepath " + text);
				}
			}
		}

		public static void RegisterUpdateDelegate(Action updateDelegate)
		{
			if (updateDelegate != null && !Instance.m_updateDelegates.Contains(updateDelegate))
			{
				Instance.m_updateDelegates.Add(updateDelegate);
			}
		}

		public static void UnregisterUpdateDelegate(Action updateDelegate)
		{
			if (updateDelegate != null)
			{
				Instance.m_updateDelegates.Remove(updateDelegate);
			}
		}

		public static void RegisterLateUpdateDelegate(Action updateDelegate)
		{
			if (updateDelegate != null && !Instance.m_lateUpdateDelegates.Contains(updateDelegate))
			{
				Instance.m_lateUpdateDelegates.Add(updateDelegate);
			}
		}

		public static void UnregisterLateUpdateDelegate(Action updateDelegate)
		{
			if (updateDelegate != null)
			{
				Instance.m_lateUpdateDelegates.Remove(updateDelegate);
			}
		}

		public static void RegisterOnGUIDelegate(Action GUIDelegate)
		{
			if (GUIDelegate != null)
			{
				if (!Instance.m_hasGUIDelegatComponent)
				{
					Instance.m_GUIDelegateComponent = Instance.gameObject.AddComponent<OnGUIDelegateComponent>();
					Instance.m_hasGUIDelegatComponent = true;
				}
				Instance.m_GUIDelegateComponent.AddOnGUIDelegate(GUIDelegate);
			}
		}

		public static void UnregisterOnGUIDelegate(Action GUIDelegate)
		{
			if (GUIDelegate != null && Instance.m_hasGUIDelegatComponent)
			{
				Instance.m_GUIDelegateComponent.RemoveOnGUIDelegate(GUIDelegate);
			}
		}

		public static bool ScheduleCallback(float secondsToWait, bool realTime, ScheduledCallback cb, object userData = null)
		{
			return s_instance.ScheduleCallbackInternal(secondsToWait, realTime, cb, userData);
		}

		public static bool CancelScheduledCallback(ScheduledCallback cb, object userData = null)
		{
			return s_instance.CancelScheduledCallbackInternal(cb, userData);
		}

		public static Coroutine RunCoroutine(IEnumerator routine, object objectRef = null)
		{
			if (s_instance.m_coroutines == null)
			{
				s_instance.m_coroutines = new Map<object, List<IEnumerator>>();
			}
			List<IEnumerator> value = null;
			if (objectRef != null)
			{
				if (s_instance.m_coroutines.TryGetValue(objectRef, out value))
				{
					value.Add(routine);
				}
				else
				{
					List<IEnumerator> list = new List<IEnumerator>();
					list.Add(routine);
					s_instance.m_coroutines.Add(objectRef, list);
				}
			}
			return s_instance.StartCoroutine(routine);
		}

		public static void CancelCoroutine(IEnumerator routine)
		{
			s_instance.StopCoroutine(routine);
		}

		public static void CancelCoroutine(Coroutine routine)
		{
			s_instance.StopCoroutine(routine);
		}

		public static void StopAllCoroutinesWithObjectRef(object objectRef)
		{
			List<IEnumerator> value = null;
			if (!s_instance.m_coroutines.TryGetValue(objectRef, out value))
			{
				return;
			}
			foreach (IEnumerator item in value)
			{
				s_instance.StopCoroutine(item);
			}
			s_instance.m_coroutines.Remove(objectRef);
		}

		public static void StopCoroutines()
		{
			s_instance.StopAllCoroutines();
			s_instance.m_coroutines.Clear();
		}

		public static bool QueueJob(JobDefinition job)
		{
			return JobQueue.QueueJob(job);
		}

		public static bool QueueJobIfNotExist(JobDefinition job)
		{
			return JobQueue.QueueJobIfNotExist(job);
		}

		public static JobDefinition QueueJob(string id, IEnumerator<IAsyncJobResult> jobAction, params IJobDependency[] dependencies)
		{
			return JobQueue.QueueJob(id, jobAction, dependencies);
		}

		public static JobDefinition QueueJobIfNotExist(string id, IEnumerator<IAsyncJobResult> jobAction, params IJobDependency[] dependencies)
		{
			return JobQueue.QueueJobIfNotExist(id, jobAction, dependencies);
		}

		public static JobDefinition QueueJob(string id, IEnumerator<IAsyncJobResult> jobAction, JobFlags jobFlags, params IJobDependency[] dependencies)
		{
			return JobQueue.QueueJob(id, jobAction, jobFlags, dependencies);
		}

		public static JobDefinition QueueJobIfNotExist(string id, IEnumerator<IAsyncJobResult> jobAction, JobFlags jobFlags, params IJobDependency[] dependencies)
		{
			return JobQueue.QueueJobIfNotExist(id, jobAction, jobFlags, dependencies);
		}

		public static void TerminateAllProcessing()
		{
			s_instance.TerminateAllProcessingInternal();
		}

		public static bool IsJobQueueMonitorEnabled()
		{
			return PlayerPrefs.GetInt("JOB_QUEUE_MONITOR", 0) != 0;
		}

		public static void SetJobQueueMonitorEnabled(bool isEnabled)
		{
			PlayerPrefs.SetInt("JOB_QUEUE_MONITOR", isEnabled ? 1 : 0);
		}

		private void EnsureJobQueueExists()
		{
			if (m_jobQueue == null)
			{
				m_jobQueue = new JobQueue(m_jobQueueFrameLimit);
				m_jobQueue.SetLogger(Log.Jobs);
				if (IsJobQueueMonitorEnabled())
				{
					JobQueueMonitor.StartMonitor(Log.Jobs, 600000f);
					m_isJobQueueMonitorEnabled = true;
				}
				m_jobQueueIterator = m_jobQueue.Process();
			}
		}

		private void ProcessUpdateDelegates()
		{
			for (int i = 0; i < m_updateDelegates.Count; i++)
			{
				if (m_updateDelegates[i] == null)
				{
					m_updateDelegates.RemoveAt(i);
					i--;
				}
				else
				{
					m_updateDelegates[i]();
				}
			}
		}

		private void ProcessLateUpdateDelegates()
		{
			for (int i = 0; i < m_lateUpdateDelegates.Count; i++)
			{
				if (m_lateUpdateDelegates[i] == null)
				{
					m_lateUpdateDelegates.RemoveAt(i);
					i--;
				}
				else
				{
					m_lateUpdateDelegates[i]();
				}
			}
		}

		private void ProcessJobQueue()
		{
			EnsureJobQueueExists();
			if (m_jobQueue != null && (m_jobQueueIterator == null || !m_jobQueueIterator.MoveNext()))
			{
				m_jobQueueIterator = m_jobQueue.Process();
			}
		}

		private void ProcessScheduledCallbacks()
		{
			lock (m_scheduledCallbacks)
			{
				float num = (m_lastRealtimeSinceStartup = Time.realtimeSinceStartup);
				if (m_scheduledCallbacks.Count == 0)
				{
					return;
				}
				LinkedListNode<SchedulerContext> linkedListNode = m_scheduledCallbacks.First;
				while (linkedListNode != null)
				{
					SchedulerContext value = linkedListNode.Value;
					if (value.m_realTime)
					{
						value.m_secondsWaited = num - value.m_startTime;
					}
					else
					{
						value.m_secondsWaited += Time.deltaTime;
					}
					if (value.m_secondsWaited >= value.m_secondsToWait)
					{
						m_readyCallbacks.AddLast(value);
						LinkedListNode<SchedulerContext> next = linkedListNode.Next;
						m_scheduledCallbacks.Remove(linkedListNode);
						linkedListNode = next;
					}
					else if (!GeneralUtils.IsCallbackValid(value.m_callback))
					{
						LinkedListNode<SchedulerContext> next2 = linkedListNode.Next;
						m_scheduledCallbacks.Remove(linkedListNode);
						linkedListNode = next2;
					}
					else
					{
						linkedListNode = linkedListNode.Next;
					}
				}
			}
			foreach (SchedulerContext readyCallback in m_readyCallbacks)
			{
				if (GeneralUtils.IsCallbackValid(readyCallback.m_callback))
				{
					readyCallback.m_callback(readyCallback.m_userData);
				}
			}
			m_readyCallbacks.Clear();
		}

		private bool ScheduleCallbackInternal(float secondsToWait, bool realTime, ScheduledCallback cb, object userData = null)
		{
			lock (m_scheduledCallbacks)
			{
				foreach (SchedulerContext scheduledCallback in m_scheduledCallbacks)
				{
					if (!(scheduledCallback.m_callback != cb) && scheduledCallback.m_userData == userData)
					{
						return false;
					}
				}
				SchedulerContext schedulerContext = new SchedulerContext();
				schedulerContext.m_startTime = m_lastRealtimeSinceStartup;
				schedulerContext.m_secondsToWait = secondsToWait;
				schedulerContext.m_realTime = realTime;
				schedulerContext.m_callback = cb;
				schedulerContext.m_userData = userData;
				float num = schedulerContext.EstimateTargetTime();
				bool flag = false;
				for (LinkedListNode<SchedulerContext> linkedListNode = m_scheduledCallbacks.Last; linkedListNode != null; linkedListNode = linkedListNode.Previous)
				{
					if (linkedListNode.Value.EstimateTargetTime() <= num)
					{
						flag = true;
						m_scheduledCallbacks.AddAfter(linkedListNode, schedulerContext);
						break;
					}
				}
				if (!flag)
				{
					m_scheduledCallbacks.AddFirst(schedulerContext);
				}
			}
			return true;
		}

		private bool CancelScheduledCallbackInternal(ScheduledCallback cb, object userData = null)
		{
			lock (m_scheduledCallbacks)
			{
				if (m_scheduledCallbacks.Count == 0)
				{
					return false;
				}
				for (LinkedListNode<SchedulerContext> linkedListNode = m_scheduledCallbacks.First; linkedListNode != null; linkedListNode = linkedListNode.Next)
				{
					SchedulerContext value = linkedListNode.Value;
					if (value.m_callback == cb && value.m_userData == userData)
					{
						m_scheduledCallbacks.Remove(linkedListNode);
						return true;
					}
				}
				return false;
			}
		}

		private void TerminateAllProcessingInternal()
		{
			m_updateDelegates.Clear();
			UnityEngine.Object.Destroy(m_GUIDelegateComponent);
			if (m_jobQueue != null)
			{
				m_jobQueue.ClearJobs();
				m_jobQueueIterator = m_jobQueue.Process();
			}
			lock (m_scheduledCallbacks)
			{
				m_scheduledCallbacks.Clear();
			}
			StopAllCoroutines();
			if (m_coroutines != null)
			{
				m_coroutines.Clear();
			}
		}
	}
}
