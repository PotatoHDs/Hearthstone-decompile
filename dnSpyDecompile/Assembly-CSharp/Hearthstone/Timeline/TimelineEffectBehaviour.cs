using System;
using UnityEngine;
using UnityEngine.Playables;

namespace Hearthstone.Timeline
{
	// Token: 0x020010F6 RID: 4342
	[Serializable]
	public abstract class TimelineEffectBehaviour<T> : PlayableBehaviour where T : TimelineEffectHelper
	{
		// Token: 0x17000D1A RID: 3354
		// (get) Token: 0x0600BE49 RID: 48713 RVA: 0x003A092D File Offset: 0x0039EB2D
		// (set) Token: 0x0600BE4A RID: 48714 RVA: 0x003A0935 File Offset: 0x0039EB35
		private protected T Helper { protected get; private set; }

		// Token: 0x0600BE4B RID: 48715 RVA: 0x003A0940 File Offset: 0x0039EB40
		protected void SpawnHelper(GameObject target, float duration)
		{
			if (this.Helper == null)
			{
				object[] helperInitializationData = this.GetHelperInitializationData();
				if (helperInitializationData != null)
				{
					this.Helper = TimelineEffectHelper.Spawn<T>(target, duration, helperInitializationData);
				}
			}
		}

		// Token: 0x0600BE4C RID: 48716
		protected abstract object[] GetHelperInitializationData();

		// Token: 0x0600BE4D RID: 48717 RVA: 0x003A0978 File Offset: 0x0039EB78
		public override void OnGraphStop(Playable playable)
		{
			T t = this.Helper;
			if (t == null)
			{
				return;
			}
			t.Kill();
		}

		// Token: 0x0600BE4E RID: 48718 RVA: 0x003A0978 File Offset: 0x0039EB78
		public override void OnBehaviourDelay(Playable playable, FrameData info)
		{
			T t = this.Helper;
			if (t == null)
			{
				return;
			}
			t.Kill();
		}

		// Token: 0x0600BE4F RID: 48719 RVA: 0x003A0978 File Offset: 0x0039EB78
		public override void OnBehaviourPause(Playable playable, FrameData info)
		{
			T t = this.Helper;
			if (t == null)
			{
				return;
			}
			t.Kill();
		}

		// Token: 0x0600BE50 RID: 48720
		protected abstract void InitializeFrame(Playable playable, FrameData info, object playerData);

		// Token: 0x0600BE51 RID: 48721
		protected abstract void UpdateFrame(Playable playable, FrameData info, object playerData, float normalizedTime);

		// Token: 0x0600BE52 RID: 48722 RVA: 0x003A0990 File Offset: 0x0039EB90
		public override void ProcessFrame(Playable playable, FrameData info, object playerData)
		{
			this.InitializeFrame(playable, info, playerData);
			this.SpawnHelper(Camera.main.gameObject, (float)playable.GetDuration<Playable>());
			if (this.Helper != null)
			{
				float num = (float)playable.GetTime<Playable>() / (float)playable.GetDuration<Playable>();
				if (num < 1f)
				{
					this.UpdateFrame(playable, info, playerData, num);
					return;
				}
				T t = this.Helper;
				if (t == null)
				{
					return;
				}
				t.Kill();
			}
		}
	}
}
