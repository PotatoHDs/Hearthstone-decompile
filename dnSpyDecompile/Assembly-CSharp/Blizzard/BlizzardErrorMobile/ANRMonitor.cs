using System;
using System.Collections;
using System.Threading;
using UnityEngine;

namespace Blizzard.BlizzardErrorMobile
{
	// Token: 0x02001210 RID: 4624
	internal class ANRMonitor
	{
		// Token: 0x140000D6 RID: 214
		// (add) Token: 0x0600CF8A RID: 53130 RVA: 0x003DC528 File Offset: 0x003DA728
		// (remove) Token: 0x0600CF8B RID: 53131 RVA: 0x003DC560 File Offset: 0x003DA760
		public event Action Detected;

		// Token: 0x140000D7 RID: 215
		// (add) Token: 0x0600CF8C RID: 53132 RVA: 0x003DC598 File Offset: 0x003DA798
		// (remove) Token: 0x0600CF8D RID: 53133 RVA: 0x003DC5D0 File Offset: 0x003DA7D0
		public event Action FirstUpdateAfterANR;

		// Token: 0x0600CF8E RID: 53134 RVA: 0x003DC608 File Offset: 0x003DA808
		public ANRMonitor(float waitLimitSeconds, float throttle, MonoBehaviour monoBehaviour)
		{
			if (waitLimitSeconds < 3f)
			{
				ExceptionLogger.LogWarning("ANRMonitor: Wait time({0}) is too short, ANRMonitor is off", new object[]
				{
					waitLimitSeconds
				});
				return;
			}
			if (throttle <= 0f)
			{
				ExceptionLogger.LogWarning("ANRMonitor: Throttle value({0}) is not positive, ANRMonitor is off", new object[]
				{
					throttle
				});
				return;
			}
			this.SetWaitSeconds(waitLimitSeconds, throttle);
			try
			{
				monoBehaviour.StartCoroutine(this.TimeRecorderProc());
			}
			catch (Exception ex)
			{
				ExceptionLogger.LogError("ANRMonitor: failed to run coroutine, ANRMonitor is off: {0}", new object[]
				{
					ex.Message
				});
				this.IsTerminated = true;
				return;
			}
			this.m_thread = new Thread(new ThreadStart(this.MonitorLoop));
			this.m_thread.Start();
		}

		// Token: 0x0600CF8F RID: 53135 RVA: 0x003DC714 File Offset: 0x003DA914
		public void OnPause(bool pauseStatus)
		{
			this.m_paused = pauseStatus;
			if (!this.m_paused)
			{
				this.ready.Set();
			}
		}

		// Token: 0x0600CF90 RID: 53136 RVA: 0x003DC730 File Offset: 0x003DA930
		public void SetWaitSeconds(float waitLimitSeconds, float throttle)
		{
			if (waitLimitSeconds > 0f && throttle > 0f)
			{
				this.m_waitLimitMilliSeconds = (int)((double)waitLimitSeconds * 1000.0);
				this.m_maxSleepTimeMilliSeconds = this.m_waitLimitMilliSeconds - 1000;
				this.m_recordSeconds = Math.Min(waitLimitSeconds / 2f, 2f);
				this.m_throttle = throttle;
				this.IsTerminated = false;
				return;
			}
			ExceptionLogger.LogInfo("ANRMonitor: Terminated by wait-seconds({0}) and throttle({1})", new object[]
			{
				waitLimitSeconds,
				throttle
			});
			this.IsTerminated = true;
		}

		// Token: 0x1700104F RID: 4175
		// (get) Token: 0x0600CF91 RID: 53137 RVA: 0x003DC7C1 File Offset: 0x003DA9C1
		// (set) Token: 0x0600CF92 RID: 53138 RVA: 0x003DC7C9 File Offset: 0x003DA9C9
		public bool IsTerminated { get; private set; } = true;

		// Token: 0x0600CF93 RID: 53139 RVA: 0x003DC7D4 File Offset: 0x003DA9D4
		private void RefreshCalledTime()
		{
			if (Monitor.TryEnter(this.m_timeLock, this.m_timeout))
			{
				try
				{
					this.m_calledTime = DateTime.UtcNow;
					return;
				}
				finally
				{
					Monitor.Exit(this.m_timeLock);
				}
			}
			ExceptionLogger.LogWarning("ANRMonitor: m_timeLock is not released. ignore it.", Array.Empty<object>());
			this.m_calledTime = DateTime.UtcNow;
		}

		// Token: 0x0600CF94 RID: 53140 RVA: 0x003DC838 File Offset: 0x003DAA38
		private IEnumerator TimeRecorderProc()
		{
			while (!this.IsTerminated)
			{
				yield return new WaitForSecondsRealtime(this.m_recordSeconds);
				if (this.m_happened)
				{
					Action firstUpdateAfterANR = this.FirstUpdateAfterANR;
					if (firstUpdateAfterANR != null)
					{
						firstUpdateAfterANR();
					}
					this.m_happened = false;
				}
				this.RefreshCalledTime();
			}
			yield break;
		}

		// Token: 0x0600CF95 RID: 53141 RVA: 0x003DC848 File Offset: 0x003DAA48
		private void MonitorLoop()
		{
			while (!this.IsTerminated)
			{
				if (this.m_paused)
				{
					ExceptionLogger.LogInfo("ANRMonitor: Paused", Array.Empty<object>());
					this.ready.Reset();
					this.ready.Wait();
					this.RefreshCalledTime();
				}
				if (!Monitor.TryEnter(this.m_timeLock, this.m_timeout))
				{
					ExceptionLogger.LogWarning("ANRMonitor: m_timeLock is not released. wait more time.", Array.Empty<object>());
					Thread.Sleep(this.m_waitLimitMilliSeconds);
				}
				else
				{
					int num;
					try
					{
						num = (int)DateTime.UtcNow.Subtract(this.m_calledTime).TotalMilliseconds;
					}
					finally
					{
						Monitor.Exit(this.m_timeLock);
					}
					if (num > this.m_waitLimitMilliSeconds)
					{
						if (!this.m_happened && this.ShouldReport())
						{
							ExceptionLogger.LogInfo("ANRMonitor: No update during {0} ms", new object[]
							{
								num
							});
							Action detected = this.Detected;
							if (detected != null)
							{
								detected();
							}
							this.m_happened = true;
						}
						this.RefreshCalledTime();
					}
					else
					{
						Thread.Sleep((num > 0) ? (this.m_waitLimitMilliSeconds - num) : this.m_maxSleepTimeMilliSeconds);
					}
				}
			}
		}

		// Token: 0x0600CF96 RID: 53142 RVA: 0x003DC974 File Offset: 0x003DAB74
		private bool ShouldReport()
		{
			bool result;
			try
			{
				result = (Convert.ToDouble(this.m_throttle) > new System.Random().NextDouble());
			}
			catch (Exception ex)
			{
				ExceptionLogger.LogError("ANRMonitor: Failed to compare ANR throttle({0}): {1}", new object[]
				{
					this.m_throttle,
					ex.Message
				});
				result = false;
			}
			return result;
		}

		// Token: 0x0400A1FF RID: 41471
		private int m_waitLimitMilliSeconds = 10000;

		// Token: 0x0400A200 RID: 41472
		private int m_maxSleepTimeMilliSeconds;

		// Token: 0x0400A201 RID: 41473
		private float m_recordSeconds;

		// Token: 0x0400A202 RID: 41474
		private float m_throttle;

		// Token: 0x0400A203 RID: 41475
		private Thread m_thread;

		// Token: 0x0400A204 RID: 41476
		private bool m_paused;

		// Token: 0x0400A205 RID: 41477
		private bool m_happened;

		// Token: 0x0400A206 RID: 41478
		private DateTime m_calledTime = DateTime.UtcNow;

		// Token: 0x0400A207 RID: 41479
		private object m_timeLock = new object();

		// Token: 0x0400A208 RID: 41480
		private TimeSpan m_timeout = TimeSpan.FromMilliseconds(500.0);

		// Token: 0x0400A209 RID: 41481
		private ManualResetEventSlim ready = new ManualResetEventSlim();
	}
}
