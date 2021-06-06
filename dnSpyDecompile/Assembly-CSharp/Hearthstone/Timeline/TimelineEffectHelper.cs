using System;
using UnityEngine;

namespace Hearthstone.Timeline
{
	// Token: 0x020010F7 RID: 4343
	public abstract class TimelineEffectHelper : MonoBehaviour
	{
		// Token: 0x17000D1B RID: 3355
		// (get) Token: 0x0600BE54 RID: 48724 RVA: 0x003A0A08 File Offset: 0x0039EC08
		// (set) Token: 0x0600BE55 RID: 48725 RVA: 0x003A0A10 File Offset: 0x0039EC10
		private protected float Duration { protected get; private set; } = 1f;

		// Token: 0x17000D1C RID: 3356
		// (get) Token: 0x0600BE56 RID: 48726 RVA: 0x003A0A19 File Offset: 0x0039EC19
		// (set) Token: 0x0600BE57 RID: 48727 RVA: 0x003A0A21 File Offset: 0x0039EC21
		private protected bool ReceivedOriginalValues { protected get; private set; }

		// Token: 0x17000D1D RID: 3357
		// (get) Token: 0x0600BE58 RID: 48728 RVA: 0x003A0A2A File Offset: 0x0039EC2A
		// (set) Token: 0x0600BE59 RID: 48729 RVA: 0x003A0A32 File Offset: 0x0039EC32
		private protected bool Initialized { protected get; private set; }

		// Token: 0x0600BE5A RID: 48730 RVA: 0x003A0A3C File Offset: 0x0039EC3C
		public static T Spawn<T>(GameObject target, float duration, params object[] initializationData) where T : TimelineEffectHelper
		{
			T t = target.AddComponent<T>();
			t.Initialized = false;
			t.SyncInstancesOfThisEffectType<T>(target);
			t.Duration = duration;
			t.Initialize(initializationData);
			t.Initialized = true;
			t.SyncInstancesOfThisEffectType<T>(target);
			return t;
		}

		// Token: 0x0600BE5B RID: 48731 RVA: 0x003A0A98 File Offset: 0x0039EC98
		private void SyncInstancesOfThisEffectType<T>(GameObject target) where T : TimelineEffectHelper
		{
			foreach (T t in target.GetComponents<T>())
			{
				if (t != null && t != this)
				{
					if (this.Initialized && !t.Initialized)
					{
						t.CopyOriginalValuesFrom<TimelineEffectHelper>(this);
						t.ReceivedOriginalValues = true;
					}
					else if (!this.Initialized && t.Initialized)
					{
						this.CopyOriginalValuesFrom<T>(t);
						this.ReceivedOriginalValues = true;
					}
				}
			}
		}

		// Token: 0x0600BE5C RID: 48732
		protected abstract void CopyOriginalValuesFrom<T>(T other) where T : TimelineEffectHelper;

		// Token: 0x0600BE5D RID: 48733
		protected abstract void Initialize(params object[] values);

		// Token: 0x0600BE5E RID: 48734 RVA: 0x003A0B30 File Offset: 0x0039ED30
		private void OnDisable()
		{
			this.Kill();
		}

		// Token: 0x0600BE5F RID: 48735 RVA: 0x003A0B38 File Offset: 0x0039ED38
		private void OnDestroy()
		{
			if (this != null && base.gameObject != null)
			{
				this.ResetTarget();
			}
		}

		// Token: 0x0600BE60 RID: 48736 RVA: 0x003A0B57 File Offset: 0x0039ED57
		public void Kill()
		{
			if (this != null && base.gameObject != null)
			{
				this.ResetTarget();
				if (Application.isPlaying)
				{
					UnityEngine.Object.Destroy(this);
				}
				else
				{
					UnityEngine.Object.DestroyImmediate(this);
				}
			}
			this.OnKill();
		}

		// Token: 0x0600BE61 RID: 48737
		protected abstract void OnKill();

		// Token: 0x0600BE62 RID: 48738
		protected abstract void ResetTarget();

		// Token: 0x0600BE63 RID: 48739
		protected abstract void UpdateTarget(float normalizedTime);

		// Token: 0x0600BE64 RID: 48740 RVA: 0x003A0B91 File Offset: 0x0039ED91
		public void UpdateEffect(float timeSinceStarted)
		{
			this.UpdateEffect(timeSinceStarted, false);
		}

		// Token: 0x0600BE65 RID: 48741 RVA: 0x003A0B9C File Offset: 0x0039ED9C
		public void UpdateEffect(float timeSinceStarted, bool isScrubbing)
		{
			float num = Mathf.Clamp(timeSinceStarted / this.Duration, 0f, 1f);
			if (!Mathf.Approximately(num, 0f) && !Mathf.Approximately(num, 1f))
			{
				this.UpdateTarget(num);
				return;
			}
			this.ResetTarget();
		}

		// Token: 0x0600BE66 RID: 48742 RVA: 0x00003BE8 File Offset: 0x00001DE8
		private void Update()
		{
		}
	}
}
