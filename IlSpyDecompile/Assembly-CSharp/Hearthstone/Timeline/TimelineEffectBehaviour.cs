using System;
using UnityEngine;
using UnityEngine.Playables;

namespace Hearthstone.Timeline
{
	[Serializable]
	public abstract class TimelineEffectBehaviour<T> : PlayableBehaviour where T : TimelineEffectHelper
	{
		protected T Helper { get; private set; }

		protected void SpawnHelper(GameObject target, float duration)
		{
			if ((UnityEngine.Object)Helper == (UnityEngine.Object)null)
			{
				object[] helperInitializationData = GetHelperInitializationData();
				if (helperInitializationData != null)
				{
					Helper = TimelineEffectHelper.Spawn<T>(target, duration, helperInitializationData);
				}
			}
		}

		protected abstract object[] GetHelperInitializationData();

		public override void OnGraphStop(Playable playable)
		{
			Helper?.Kill();
		}

		public override void OnBehaviourDelay(Playable playable, FrameData info)
		{
			Helper?.Kill();
		}

		public override void OnBehaviourPause(Playable playable, FrameData info)
		{
			Helper?.Kill();
		}

		protected abstract void InitializeFrame(Playable playable, FrameData info, object playerData);

		protected abstract void UpdateFrame(Playable playable, FrameData info, object playerData, float normalizedTime);

		public override void ProcessFrame(Playable playable, FrameData info, object playerData)
		{
			InitializeFrame(playable, info, playerData);
			SpawnHelper(Camera.main.gameObject, (float)playable.GetDuration());
			if ((UnityEngine.Object)Helper != (UnityEngine.Object)null)
			{
				float num = (float)playable.GetTime() / (float)playable.GetDuration();
				if (num < 1f)
				{
					UpdateFrame(playable, info, playerData, num);
				}
				else
				{
					Helper?.Kill();
				}
			}
		}
	}
}
