using UnityEngine;

namespace Hearthstone.Timeline
{
	public abstract class TimelineEffectHelper : MonoBehaviour
	{
		protected float Duration { get; private set; } = 1f;


		protected bool ReceivedOriginalValues { get; private set; }

		protected bool Initialized { get; private set; }

		public static T Spawn<T>(GameObject target, float duration, params object[] initializationData) where T : TimelineEffectHelper
		{
			T val = target.AddComponent<T>();
			val.Initialized = false;
			val.SyncInstancesOfThisEffectType<T>(target);
			val.Duration = duration;
			val.Initialize(initializationData);
			val.Initialized = true;
			val.SyncInstancesOfThisEffectType<T>(target);
			return val;
		}

		private void SyncInstancesOfThisEffectType<T>(GameObject target) where T : TimelineEffectHelper
		{
			T[] components = target.GetComponents<T>();
			foreach (T val in components)
			{
				if ((Object)val != (Object)null && val != this)
				{
					if (Initialized && !val.Initialized)
					{
						val.CopyOriginalValuesFrom(this);
						val.ReceivedOriginalValues = true;
					}
					else if (!Initialized && val.Initialized)
					{
						CopyOriginalValuesFrom(val);
						ReceivedOriginalValues = true;
					}
				}
			}
		}

		protected abstract void CopyOriginalValuesFrom<T>(T other) where T : TimelineEffectHelper;

		protected abstract void Initialize(params object[] values);

		private void OnDisable()
		{
			Kill();
		}

		private void OnDestroy()
		{
			if (this != null && base.gameObject != null)
			{
				ResetTarget();
			}
		}

		public void Kill()
		{
			if (this != null && base.gameObject != null)
			{
				ResetTarget();
				if (Application.isPlaying)
				{
					Object.Destroy(this);
				}
				else
				{
					Object.DestroyImmediate(this);
				}
			}
			OnKill();
		}

		protected abstract void OnKill();

		protected abstract void ResetTarget();

		protected abstract void UpdateTarget(float normalizedTime);

		public void UpdateEffect(float timeSinceStarted)
		{
			UpdateEffect(timeSinceStarted, isScrubbing: false);
		}

		public void UpdateEffect(float timeSinceStarted, bool isScrubbing)
		{
			float num = Mathf.Clamp(timeSinceStarted / Duration, 0f, 1f);
			if (!Mathf.Approximately(num, 0f) && !Mathf.Approximately(num, 1f))
			{
				UpdateTarget(num);
			}
			else
			{
				ResetTarget();
			}
		}

		private void Update()
		{
		}
	}
}
